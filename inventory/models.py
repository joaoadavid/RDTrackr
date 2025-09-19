from django.db import models
from django.utils import timezone

class Item(models.Model):
    sku = models.CharField("SKU", max_length=40, unique=True)
    descricao = models.CharField("Descrição", max_length=180)
    unidade = models.CharField("Unidade", max_length=15, default="un")
    estoque_minimo = models.DecimalField("Estoque mínimo", max_digits=12, decimal_places=2, default=0)
    is_active = models.BooleanField("Ativo", default=True)

    # Campo derivado (opcional). Se não quiser denormalizar, remova e calcule via agregação.
    stock = models.DecimalField("Saldo atual", max_digits=14, decimal_places=2, default=0)

    created_at = models.DateTimeField(default=timezone.now, editable=False)
    updated_at = models.DateTimeField(auto_now=True)

    class Meta:
        ordering = ["sku"]

    def __str__(self):
        return f"{self.sku} - {self.descricao}"
