from django.urls import path
from .views import RegisterView, AuthLoginView, LogoutStrictView

app_name = "authors"

urlpatterns = [
    path("register/", RegisterView.as_view(), name="register"),
    path("login/",    AuthLoginView.as_view(), name="login"),
    path("logout/",   LogoutStrictView.as_view(), name="logout"),
]
