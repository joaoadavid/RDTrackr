from django.views.generic import TemplateView, FormView
from django.urls import reverse_lazy
from django.contrib import messages
from inventory.models import Item
from movements.models import Movement
from .forms import ContactForm

class PublicBase(TemplateView):
    """Base apenas para organizar, sem exigência de login."""
    template_name = ""  # overridden

class HomeView(PublicBase):
    template_name = "home/index.html"
    def get_context_data(self, **kwargs):
        ctx = super().get_context_data(**kwargs)
        if self.request.user.is_authenticated:
            ctx["stats"] = {
                "items": Item.objects.count(),
                "movements": Movement.objects.count(),
            }
        return ctx

class SolutionsView(PublicBase):
    template_name = "home/solutions.html"

class SegmentsView(PublicBase):
    template_name = "home/segments.html"

class PlansView(PublicBase):
    template_name = "home/plans.html"

class HelpView(PublicBase):
    template_name = "home/help.html"

class ContactView(FormView):
    template_name = "home/contact.html"
    form_class = ContactForm
    success_url = reverse_lazy("home:contact")

    def form_valid(self, form):
        # Aqui você pode integrar e-mail (send_mail) depois.
        messages.success(self.request, "Mensagem enviada! Responderemos em breve.")
        return super().form_valid(form)
