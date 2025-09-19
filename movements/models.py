from django.db import models, transaction
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
        return f"{self.item.code} {s}{self.quantity} ({self.created_at:%d/%m/%Y})"

    def apply_to_item(self):
        """Aplica a movimentação ao saldo do item."""
        delta = self.quantity if self.kind == self.IN else -self.quantity
        self.item.stock = (self.item.stock or 0) + delta
        if self.item.stock < 0:
            raise ValueError("Saldo resultante negativo para o item.")
        self.item.save(update_fields=["stock", "updated_at"])

    def clean(self):
        if self.quantity <= 0:
            from django.core.exceptions import ValidationError
            raise ValidationError({"quantity": "Quantidade deve ser positiva."})

    def save(self, *args, **kwargs):
        is_create = self._state.adding
        with transaction.atomic():
            super().save(*args, **kwargs)
            if is_create:
                self.apply_to_item()
