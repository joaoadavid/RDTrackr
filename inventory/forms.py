# inventory/forms.py
from decimal import Decimal
from django import forms
from .models import Item

class ItemForm(forms.ModelForm):
    initial_stock = forms.DecimalField(
        label="Saldo inicial", required=False, min_value=0,
        decimal_places=2, max_digits=14,
        widget=forms.NumberInput(attrs={"class":"input input-bordered","step":"0.01"})
    )

    class Meta:
        model = Item
        fields = ["sku","descricao","unidade","stock","estoque_minimo","is_active"]
        widgets = {
            "sku": forms.TextInput(attrs={"class":"input input-bordered"}),
            "descricao": forms.TextInput(attrs={"class":"input input-bordered"}),
            "unidade": forms.TextInput(attrs={"class":"input input-bordered"}),
            "stock": forms.NumberInput(attrs={"class":"input input-bordered","step":"0.01"}),
            "estoque_minimo": forms.NumberInput(attrs={"class":"input input-bordered","step":"0.01"}),
            "is_active": forms.CheckboxInput(attrs={"class":"toggle"}),
        }

    def save(self, commit=True):
        obj = super().save(commit=commit)
        # guarda para a view criar uma Movement IN
        obj._initial_stock = self.cleaned_data.get("initial_stock") or Decimal("0")
        return obj
