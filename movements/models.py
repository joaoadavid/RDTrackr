# movements/models.py
from decimal import Decimal
from django.db import models, transaction
from django.db.models import F
from django.contrib.auth import get_user_model
from django.utils import timezone
from django.core.exceptions import ValidationError
from inventory.models import Item

User = get_user_model()

class Movement(models.Model):
    IN = "IN"
    OUT = "OUT"
    KIND_CHOICES = [(IN, "Entrada"), (OUT, "Saída")]

    item = models.ForeignKey(Item, on_delete=models.PROTECT, related_name="movements")
    kind = models.CharField(max_length=3, choices=KIND_CHOICES)
    quantity = models.DecimalField(max_digits=14, decimal_places=2)
    note = models.CharField(max_length=255, blank=True)
    created_by = models.ForeignKey(User, on_delete=models.PROTECT, related_name="created_movements")
    created_at = models.DateTimeField(default=timezone.now, editable=False)

    class Meta:
        ordering = ["-created_at"]

    def __str__(self):
        s = "+" if self.kind == self.IN else "-"
        return f"{self.item.sku} {s}{self.quantity} ({self.created_at:%d/%m/%Y})"

    def clean(self):
        # quantidade válida
        if self.quantity is None or self.quantity <= 0:
            raise ValidationError({"quantity": "Quantidade deve ser positiva."})

        # 🔒 item deve estar ativo
        if self.item_id:
            # usa getattr pra evitar falha se o atributo mudar de nome
            if not getattr(self.item, "is_active", True):
                # manda pro topo do form (non_field_errors) com mensagem clara
                raise ValidationError("Este item está inativo e não pode receber movimentações.")

        # estoque suficiente nas saídas
        if self.kind == self.OUT and self.item_id:
            current = self.item.stock or Decimal("0")
            if self.quantity > current:
                raise ValidationError(
                    f"Estoque insuficiente. Disponível: {current}, solicitado: {self.quantity}."
                )

    def apply_to_item(self):
        """Aplica a movimentação ao saldo do item com lock de linha."""
        delta = self.quantity if self.kind == self.IN else -self.quantity

        # bloqueia a linha do item nesta transação
        item = Item.objects.select_for_update().get(pk=self.item_id)

        # 🔒 checagem extra sob lock: não movimenta item inativo
        if not getattr(item, "is_active", True):
            raise ValueError("Não é possível movimentar um item inativo.")

        new_stock = (item.stock or Decimal("0")) + delta
        if new_stock < 0:
            # revalidação sob lock (prevenção contra corrida)
            raise ValueError("Saldo resultante negativo para o item.")

        # atualiza via F() para evitar race conditions
        Item.objects.filter(pk=item.pk).update(
            stock=F("stock") + delta,
            updated_at=timezone.now(),
        )

    def save(self, *args, **kwargs):
        is_create = self._state.adding
        with transaction.atomic():
            # garante validações mesmo fora de ModelForm
            self.full_clean()
            super().save(*args, **kwargs)
            if is_create:
                self.apply_to_item()
