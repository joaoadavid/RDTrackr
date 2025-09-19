from django.contrib import admin
from .models import Item

@admin.register(Item)
class ItemAdmin(admin.ModelAdmin):
    list_display = ("sku", "descricao", "unidade", "stock", "estoque_minimo", "is_active")
    list_filter = ("is_active", "unidade")
    search_fields = ("sku", "descricao")
    ordering = ("sku",)
