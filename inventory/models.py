# inventory/models.py
from django.db import models
from django.db.models import Q

class Item(models.Model):
    sku = models.CharField(max_length=50, unique=True)
    descricao = models.CharField(max_length=255)
    unidade = models.CharField(max_length=20, blank=True)           # ex.: UN, KG...
    stock = models.DecimalField(max_digits=14, decimal_places=2, default=0)
    estoque_minimo = models.DecimalField(max_digits=14, decimal_places=2, default=0)
    is_active = models.BooleanField(default=True)
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)

    class Meta:
        ordering = ["descricao", "sku"]
        constraints = [
            # opcional (requer migration): evita estoque negativo no banco
            models.CheckConstraint(
                check=Q(stock__gte=0),
                name="item_stock_non_negative",
            )
        ]

    def __str__(self):
        return f"{self.sku} — {self.descricao}"
