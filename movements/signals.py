from django.db.models.signals import post_save
from django.dispatch import receiver
from .models import Movimento

@receiver(post_save, sender=Movimento)
def atualizar_saldo(sender, instance, created, **kwargs):
    if created:
        instance.aplicar_no_saldo()
