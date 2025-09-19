from django.urls import path
from .views import (
    ItemList,
    ItemCreate,
    ItemDetail,
    ItemUpdate,
    ExportItemsCSVView,
)

app_name = "inventory"

urlpatterns = [
    path("", ItemList.as_view(), name="item-list"),
    path("new/", ItemCreate.as_view(), name="item-create"),
    path("<int:pk>/", ItemDetail.as_view(), name="item-detail"),
    path("<int:pk>/edit/", ItemUpdate.as_view(), name="item-edit"),  # 👈 novo
    path("export.csv", ExportItemsCSVView.as_view(), name="item-export"),
]
