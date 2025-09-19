from django.urls import path
from .views import MovementListView, MovementCreateView

app_name = "movements"

urlpatterns = [
    path("", MovementListView.as_view(), name="movement-list"),
    path("new/", MovementCreateView.as_view(), name="movement-create"),
]
