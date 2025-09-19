from django.urls import path
from .views import OverviewView

app_name = "reports"

urlpatterns = [
    path("overview/", OverviewView.as_view(), name="overview"),
]
