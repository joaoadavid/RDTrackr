import { useState } from "react";
import { Plus } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { useToast } from "@/hooks/use-toast";
import { NewMovementDialog } from "@/components/movements/NewMovementDialog";

const mockMovements = [
  {
    id: 1,
    type: "INBOUND",
    reference: "PO-001",
    product: "Notebook Dell",
    warehouse: "Depósito Principal",
    quantity: 50,
    createdAt: "2025-10-30 14:30",
    user: "Admin",
  },
  {
    id: 2,
    type: "OUTBOUND",
    reference: "ORD-1234",
    product: "Mouse Logitech",
    warehouse: "CD Rio",
    quantity: -15,
    createdAt: "2025-10-30 12:15",
    user: "Vendas",
  },
  {
    id: 3,
    type: "ADJUST",
    reference: "ADJ-005",
    product: "Teclado Mecânico",
    warehouse: "Depósito Principal",
    quantity: -3,
    createdAt: "2025-10-29 16:45",
    user: "Estoque",
  },
  {
    id: 4,
    type: "INBOUND",
    reference: "PO-002",
    product: "Monitor LG",
    warehouse: "CD Sul",
    quantity: 25,
    createdAt: "2025-10-29 10:20",
    user: "Admin",
  },
  {
    id: 5,
    type: "OUTBOUND",
    reference: "ORD-1235",
    product: "Webcam HD",
    warehouse: "Depósito Principal",
    quantity: -8,
    createdAt: "2025-10-28 15:30",
    user: "Vendas",
  },
];

const movementTypeMap = {
  INBOUND: { label: "Entrada", variant: "default" as const },
  OUTBOUND: { label: "Saída", variant: "secondary" as const },
  ADJUST: { label: "Ajuste", variant: "outline" as const },
};

export default function Movements() {
  const [typeFilter, setTypeFilter] = useState<string>("all");
  const [movements, setMovements] = useState(mockMovements);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const { toast } = useToast();

  const filteredMovements = movements.filter(
    (movement) => typeFilter === "all" || movement.type === typeFilter
  );

  const handleAddMovement = (movement: any) => {
    setMovements((prev) => [...prev, movement]);
    toast({
      title: "Movimentação adicionada",
      description: `A movimentação ${movement.reference} foi registrada com sucesso.`,
    });
  };

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">Movimentações</h2>
          <p className="text-muted-foreground">
            Histórico de entradas, saídas e ajustes
          </p>
        </div>
        <Button onClick={() => setIsDialogOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Nova Movimentação
        </Button>
      </div>

      {/* Modal de Nova Movimentação */}
      <NewMovementDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        onCreate={handleAddMovement}
      />

      <Card>
        <CardHeader>
          <CardTitle>Histórico de Movimentações</CardTitle>
          <CardDescription>
            Todas as alterações de estoque registradas
          </CardDescription>
        </CardHeader>
        <CardContent>
          <div className="mb-4 flex gap-4">
            <Select value={typeFilter} onValueChange={setTypeFilter}>
              <SelectTrigger className="w-[200px]" data-testid="filter-trigger">
                <SelectValue placeholder="Tipo" />
              </SelectTrigger>
              <SelectContent data-testid="filter-menu">
                <SelectItem value="all" data-testid="filter-all">
                  Todos
                </SelectItem>
                <SelectItem value="INBOUND" data-testid="filter-inbound">
                  Entradas
                </SelectItem>
                <SelectItem value="OUTBOUND" data-testid="filter-outbound">
                  Saídas
                </SelectItem>
                <SelectItem value="ADJUST" data-testid="filter-adjust">
                  Ajustes
                </SelectItem>
              </SelectContent>
            </Select>
          </div>

          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Tipo</TableHead>
                <TableHead>Referência</TableHead>
                <TableHead>Produto</TableHead>
                <TableHead>Depósito</TableHead>
                <TableHead className="text-right">Quantidade</TableHead>
                <TableHead>Data/Hora</TableHead>
                <TableHead>Usuário</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {filteredMovements.map((movement) => (
                <TableRow key={movement.id}>
                  <TableCell>
                    <Badge
                      variant={
                        movementTypeMap[
                          movement.type as keyof typeof movementTypeMap
                        ].variant
                      }
                    >
                      {
                        movementTypeMap[
                          movement.type as keyof typeof movementTypeMap
                        ].label
                      }
                    </Badge>
                  </TableCell>
                  <TableCell className="font-medium">
                    {movement.reference}
                  </TableCell>
                  <TableCell>{movement.product}</TableCell>
                  <TableCell>{movement.warehouse}</TableCell>
                  <TableCell
                    className={`text-right font-medium ${
                      movement.quantity > 0
                        ? "text-success"
                        : "text-destructive"
                    }`}
                  >
                    {movement.quantity > 0 ? "+" : ""}
                    {movement.quantity}
                  </TableCell>
                  <TableCell>{movement.createdAt}</TableCell>
                  <TableCell>{movement.user}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </div>
  );
}
