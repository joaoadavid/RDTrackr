from django.conf import settings
from django.conf.urls.static import static
from django.contrib import admin
from django.urls import include, path

urlpatterns = [
    path("", include(("home.urls", "home"), namespace="home")),  # ← nova home na raiz
    path("recipes/", include("recipes.urls")),                  # antes estava em "", agora /recipes/
    path("authors/", include(("authors.urls", "authors"), namespace="authors")),
    path("inventory/", include(("inventory.urls", "inventory"), namespace="inventory")),
    path("movements/", include(("movements.urls", "movements"), namespace="movements")),
    path("admin/", admin.site.urls),
]

if settings.DEBUG:
    urlpatterns += static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
    urlpatterns += static(settings.STATIC_URL, document_root=settings.STATIC_ROOT)
