# inventory/views.py
from django.views.generic import ListView, CreateView, DetailView, View
from django.urls import reverse_lazy
from django.contrib.auth.mixins import LoginRequiredMixin
from django.db.models import Q, Sum, Case, When, F, DecimalField
from django.http import HttpResponse
import csv

from inventory.models import Item

# Se você tiver o app 'movements' com Movement (item, kind, quantity),
# esta importação funciona. Se não tiver ainda, comente a linha e ajuste o CSV.
try:
    from movements.models import Movement
    HAS_MOVEMENTS = True
except Exception:
    HAS_MOVEMENTS = False


class ItemList(LoginRequiredMixin, ListView):
    model = Item
    paginate_by = 20
    ordering = ["sku"]
    context_object_name = "items"  # opcional; use 'items' no template

    def get_queryset(self):
        qs = super().get_queryset()
        q = (self.request.GET.get("q") or "").strip()
        if q:
            # Busca por SKU ou descrição (OR) — sem perder a ordenação
            qs = qs.filter(
                Q(sku__icontains=q) | Q(descricao__icontains=q)
            )
        return qs

    def get_context_data(self, **kwargs):
        ctx = super().get_context_data(**kwargs)
        ctx["q"] = self.request.GET.get("q", "")
        return ctx


class ItemCreate(LoginRequiredMixin, CreateView):
    model = Item
    fields = ["sku", "descricao", "unidade", "estoque_minimo"]
    success_url = reverse_lazy("inventory:item-list")


class ItemDetail(LoginRequiredMixin, DetailView):
    model = Item

from django.views.generic import UpdateView
# ...demais imports já existentes...

class ItemUpdate(LoginRequiredMixin, UpdateView):
    model = Item
    fields = ["sku", "descricao", "unidade", "estoque_minimo"]
    template_name = "inventory/item_form.html"  # vamos reutilizar p/ criar/editar
    success_url = reverse_lazy("inventory:item-list")


class ExportItemsCSVView(LoginRequiredMixin, View):
    """
    Exporta itens em CSV. Se houver 'movements.Movement', calcula o saldo:
      stock = sum(IN) - sum(OUT)
    Caso contrário, exporta 'saldo' como vazio.
    """
    def get(self, request):
        # CSV compatível com Excel/LibreOffice
        resp = HttpResponse(content_type="text/csv; charset=utf-8")
        resp["Content-Disposition"] = 'attachment; filename="itens.csv"'
        writer = csv.writer(resp)
        writer.writerow(["sku", "descricao", "unidade", "estoque_minimo", "saldo"])

        if HAS_MOVEMENTS:
            # Agrega saldos por item numa subconsulta
            stocks = (
                Movement.objects
                .values("item_id")
                .annotate(
                    saldo=Sum(
                        Case(
                            When(kind="IN", then=F("quantity")),
                            When(kind="OUT", then=-F("quantity")),
                            default=0,
                            output_field=DecimalField(max_digits=14, decimal_places=2),
                        )
                    )
                )
            )
            # Transforma em dict {item_id: saldo}
            stock_map = {row["item_id"]: row["saldo"] or 0 for row in stocks}
        else:
            stock_map = {}

        for it in Item.objects.all().order_by("sku"):
            saldo = stock_map.get(it.id, "")
            writer.writerow([it.sku, it.descricao, it.unidade, it.estoque_minimo, saldo])

        return resp
