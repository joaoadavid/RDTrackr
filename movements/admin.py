from django.contrib import admin
from .models import Movement

@admin.register(Movement)
class MovementAdmin(admin.ModelAdmin):
    list_display = ("item", "kind", "quantity", "created_by", "created_at")
    list_filter = ("kind", "created_at")
    search_fields = ("item__code", "item__name", "note")
    autocomplete_fields = ("item", "created_by")
