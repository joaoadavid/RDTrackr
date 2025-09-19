from django import forms
from .models import Movement

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
                "min": "0",
            }),
            "note": forms.Textarea(attrs={"class": "textarea textarea-bordered", "rows": 3}),
        }
