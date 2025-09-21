# movements/views.py
from django.contrib.auth.mixins import LoginRequiredMixin
from django.views.generic import ListView, CreateView
from django.db.models import Q
from django.urls import reverse_lazy
from django.core.exceptions import ValidationError
from .models import Movement
from .forms import MovementForm

class MovementListView(LoginRequiredMixin, ListView):
    model = Movement
    template_name = "movements/movement_list.html"
    context_object_name = "page_obj"
    paginate_by = 20

    def get_queryset(self):
        qs = (Movement.objects
              .select_related("item", "created_by")
              .order_by("-created_at"))

        q = self.request.GET.get("q") or ""
        kind = self.request.GET.get("kind") or ""
        if q:
            qs = qs.filter(
                Q(item__sku__icontains=q) |
                Q(item__descricao__icontains=q) |
                Q(note__icontains=q)
            )
        if kind in ("IN", "OUT"):
            qs = qs.filter(kind=kind)
        return qs

    def get_context_data(self, **kwargs):
        ctx = super().get_context_data(**kwargs)
        ctx["q"] = self.request.GET.get("q", "")
        ctx["kind"] = self.request.GET.get("kind", "")
        return ctx


class MovementCreateView(LoginRequiredMixin, CreateView):
    model = Movement
    form_class = MovementForm
    template_name = "movements/movement_form.html"
    success_url = reverse_lazy("movements:movement-list")

    def form_valid(self, form):
        form.instance.created_by = self.request.user
        try:
            return super().form_valid(form)
        except (ValueError, ValidationError) as e:
            form.add_error(None, str(e))
            return self.form_invalid(form)
        
    def get_success_url(self):
        # permite voltar para a lista/detalhe do item
        return self.request.POST.get("next") or super().get_success_url()