# inventory/urls.py
from django.urls import path
from .views import (
    ItemListView, ItemCreateView, ItemDetailView,
    ItemUpdateView, ItemDeleteView,
)

app_name = "inventory"

urlpatterns = [
    path("", ItemListView.as_view(), name="item-list"),
    path("new/", ItemCreateView.as_view(), name="item-create"),
    path("<int:pk>/", ItemDetailView.as_view(), name="item-detail"),
    path("<int:pk>/edit/", ItemUpdateView.as_view(), name="item-update"),
    path("<int:pk>/delete/", ItemDeleteView.as_view(), name="item-delete"),
]
