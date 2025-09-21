# movements/forms.py
from django import forms
from django.core.exceptions import ValidationError
from .models import Movement
from inventory.models import Item

class MovementForm(forms.ModelForm):
    class Meta:
        model = Movement
        fields = ["item", "kind", "quantity", "note"]
        widgets = {
            "item": forms.Select(attrs={"class": "select select-bordered"}),
            "kind": forms.Select(attrs={"class": "select select-bordered"}),
            "quantity": forms.NumberInput(attrs={
                "class": "input input-bordered",
                "step": "0.01",
                "min": "0.01",
            }),
            "note": forms.Textarea(attrs={"class": "textarea textarea-bordered", "rows": 3}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)

        # só itens ATIVOS no dropdown
        self.fields["item"].queryset = (
            Item.objects.filter(is_active=True)
            .order_by("descricao", "sku")
        )

        # label amigável (mantido do seu código)
        self.fields["item"].label_from_instance = (
            lambda obj: (
                f"{obj.sku} — {obj.descricao} (estoque: {obj.stock} {obj.unidade})"
                if getattr(obj, "unidade", None) else
                f"{obj.sku} — {obj.descricao} (estoque: {obj.stock})"
            )
        )

    def clean(self):
        cleaned = super().clean()
        item = cleaned.get("item")
        # garantia extra contra POST manual com item inativo
        if item and not getattr(item, "is_active", True):
            raise ValidationError("Este item está inativo e não pode receber movimentações.")
        return cleaned
