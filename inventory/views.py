# inventory/views.py
from decimal import Decimal
from django.contrib import messages
from django.contrib.auth.mixins import LoginRequiredMixin
from django.db.models import Sum, Count, Q, F
from django.shortcuts import redirect
from django.urls import reverse_lazy
from django.views.generic import ListView, CreateView, UpdateView, DetailView, DeleteView
from .models import Item
from .forms import ItemForm
from movements.models import Movement

class ItemListView(LoginRequiredMixin, ListView):
    model = Item
    template_name = "inventory/item_list.html"
    context_object_name = "items"
    paginate_by = 20

    def get_queryset(self):
        qs = Item.objects.all().order_by("descricao","sku")
        q = self.request.GET.get("q") or ""
        if q:
            qs = qs.filter(Q(descricao__icontains=q)|Q(sku__icontains=q))
        return qs

    def get_context_data(self, **kwargs):
        ctx = super().get_context_data(**kwargs)
        qs = Item.objects.all()
        ctx["stats"] = {
            "total_stock": qs.aggregate(s=Sum("stock"))["s"] or Decimal("0"),
            "ativos": qs.filter(is_active=True).count(),
            "abaixo_min": qs.filter(stock__lt=F("estoque_minimo")).count(),
            "itens": qs.count(),
        }
        ctx["q"] = self.request.GET.get("q","")
        return ctx


class ItemCreateView(LoginRequiredMixin, CreateView):
    model = Item
    form_class = ItemForm
    template_name = "inventory/item_form.html"
    success_url = reverse_lazy("inventory:item-list")

    def form_valid(self, form):
        resp = super().form_valid(form)  # salva o item
        ini = getattr(self.object, "_initial_stock", Decimal("0"))
        if ini > 0:
            # registra saldo inicial como Movement IN
            Movement.objects.create(
                item=self.object, kind=Movement.IN, quantity=ini,
                note="Saldo inicial", created_by=self.request.user
            )
        messages.success(self.request, "Item criado com sucesso.")
        return resp


class ItemUpdateView(LoginRequiredMixin, UpdateView):
    model = Item
    form_class = ItemForm
    template_name = "inventory/item_form.html"
    success_url = reverse_lazy("inventory:item-list")


class ItemDeleteView(LoginRequiredMixin, DeleteView):
    model = Item
    template_name = "inventory/item_confirm_delete.html"
    success_url = reverse_lazy("inventory:item-list")


class ItemDetailView(LoginRequiredMixin, DetailView):
    model = Item
    template_name = "inventory/item_detail.html"
    context_object_name = "item"
