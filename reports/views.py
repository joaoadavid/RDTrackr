from django.contrib.auth.mixins import LoginRequiredMixin
from django.views.generic import TemplateView
from django.db.models import Sum, F
from movements.models import Movement
from inventory.models import Item

class OverviewView(LoginRequiredMixin, TemplateView):
    template_name = "reports/overview.html"

    def get_context_data(self, **kwargs):
        ctx = super().get_context_data(**kwargs)
        ctx["total_items"] = Item.objects.filter(is_active=True).count()
        ctx["total_in"] = Movement.objects.filter(kind="IN").aggregate(s=Sum("quantity"))["s"] or 0
        ctx["total_out"] = Movement.objects.filter(kind="OUT").aggregate(s=Sum("quantity"))["s"] or 0
        ctx["low_stock"] = Item.objects.filter(is_active=True, stock__lt=F("min_level")).count()
        return ctx
