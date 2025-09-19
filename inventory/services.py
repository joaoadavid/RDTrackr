from django.db.models import Sum, Case, When, F, DecimalField
from movements.models import Movement

def compute_item_stock(item_id):
    qs = Movement.objects.filter(item_id=item_id).values("item_id").annotate(
        stock=Sum(
            Case(
                When(kind="IN", then=F("quantity")),
                When(kind="OUT", then=-F("quantity")),
                default=0,
                output_field=DecimalField(max_digits=14, decimal_places=2),
            )
        )
    )
    return qs[0]["stock"] if qs else 0
