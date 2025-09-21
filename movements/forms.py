# movements/forms.py
from django import forms
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
        self.fields["item"].queryset = Item.objects.order_by("descricao", "sku")
        self.fields["item"].label_from_instance = (
            lambda obj: (
                f"{obj.sku} — {obj.descricao} (estoque: {obj.stock} {obj.unidade})"
                if getattr(obj, "unidade", None) else
                f"{obj.sku} — {obj.descricao} (estoque: {obj.stock})"
            )
        )
