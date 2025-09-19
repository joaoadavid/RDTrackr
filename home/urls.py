from django.urls import path
from .views import (
    HomeView, SolutionsView, SegmentsView, PlansView, HelpView, ContactView
)

app_name = "home"

urlpatterns = [
    path("", HomeView.as_view(), name="index"),
    path("solucoes/",  SolutionsView.as_view(), name="solutions"),
    path("segmentos/", SegmentsView.as_view(), name="segments"),
    path("planos/",    PlansView.as_view(),    name="plans"),
    path("ajuda/",     HelpView.as_view(),     name="help"),
    path("contato/",   ContactView.as_view(),  name="contact"),
]
