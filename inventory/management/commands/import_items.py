import csv
from django.core.management.base import BaseCommand
from inventory.models import Item

class Command(BaseCommand):
    help = "Importa itens de um CSV (code,name,unit,min_level,stock)"

    def add_arguments(self, parser):
        parser.add_argument("csv_path")

    def handle(self, csv_path, **kwargs):
        with open(csv_path, newline="", encoding="utf-8") as f:
            for row in csv.DictReader(f):
                Item.objects.update_or_create(
                    code=row["code"],
                    defaults={
                        "name": row["name"],
                        "unit": row.get("unit") or "un",
                        "min_level": row.get("min_level") or 0,
                        "stock": row.get("stock") or 0,
                    }
                )
        self.stdout.write(self.style.SUCCESS("Importação concluída."))
