# movements/models.py
from decimal import Decimal
from django.db import models, transaction
from django.db.models import F
from django.core.exceptions import ValidationError
from django.contrib.auth import get_user_model
from django.utils import timezone
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
        return f"{self.item.sku} {s}{self.quantity}"

    # -------- Helpers internos --------
    def _signed_qty(self, kind=None, qty=None) -> Decimal:
        k = kind if kind is not None else self.kind
        q = Decimal(qty if qty is not None else self.quantity)
        if q <= 0:
            raise ValidationError("A quantidade deve ser maior que zero.")
        return q if k == self.IN else -q

    # -------- Validação de domínio --------
    def clean(self):
        super().clean()
        # quantity > 0
        if self.quantity is None or Decimal(self.quantity) <= 0:
            raise ValidationError({"quantity": "A quantidade deve ser maior que zero."})

    # -------- Persistência com ajuste de estoque --------
    def save(self, *args, **kwargs):
        with transaction.atomic():
            # Lock na linha do item para evitar corrida
            item_locked = Item.objects.select_for_update().get(pk=self.item_id)

            if self.pk:
                # UPDATE: calcular delta (novo - antigo)
                old = Movement.objects.select_for_update().get(pk=self.pk)
                old_signed = old._signed_qty(old.kind, old.quantity)
                new_signed = self._signed_qty(self.kind, self.quantity)
                delta = new_signed - old_signed
            else:
                # CREATE
                delta = self._signed_qty(self.kind, self.quantity)

            # Validar estoque não negativo
            # (se delta negativo e estoque insuficiente, bloqueia)
            # Obs: usamos .values_list para pegar valor atual sem refrescar objeto.
            from django.db.models import Value
            # checar valor atual
            current_stock = Item.objects.filter(pk=item_locked.pk).values_list("stock", flat=True).first() or Decimal("0")
            new_stock = Decimal(current_stock) + Decimal(delta)
            if new_stock < 0:
                raise ValidationError("Estoque insuficiente para concluir esta saída.")

            # Aplicar delta
            Item.objects.filter(pk=item_locked.pk).update(stock=F("stock") + delta)

            # Agora salva a movimentação
            super().save(*args, **kwargs)

    def delete(self, *args, **kwargs):
        with transaction.atomic():
            item_locked = Item.objects.select_for_update().get(pk=self.item_id)
            # Reverter efeito desta movimentação
            delta = -self._signed_qty(self.kind, self.quantity)
            # Validar (delta pode ser positivo ou negativo; aqui normalmente repõe estoque)
            current_stock = Item.objects.filter(pk=item_locked.pk).values_list("stock", flat=True).first() or Decimal("0")
            new_stock = Decimal(current_stock) + Decimal(delta)
            if new_stock < 0:
                # Na prática não deveria acontecer (estamos devolvendo estoque),
                # mas deixamos a defesa por consistência.
                raise ValidationError("Operação inválida: resultaria em estoque negativo.")
            Item.objects.filter(pk=item_locked.pk).update(stock=F("stock") + delta)
            super().delete(*args, **kwargs)
