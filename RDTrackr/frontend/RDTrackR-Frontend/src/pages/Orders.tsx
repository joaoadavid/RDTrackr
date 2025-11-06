import { useState } from "react";
import { Plus, MoreHorizontal, Eye, X, Check } from "lucide-react";
import { Button } from "@/components/ui/button";
import { NewOrderDialog } from "@/components/orders/NewOrderDialog";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
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

const mockSalesOrders = [
  {
    id: 1,
    number: "ORD-2025-001",
    customer: "João Silva",
    status: "PAID",
    items: 3,
    total: 4800.0,
    createdAt: "2025-10-25",
  },
  {
    id: 2,
    number: "ORD-2025-002",
    customer: "Maria Souza",
    status: "PENDING",
    items: 1,
    total: 1650.0,
    createdAt: "2025-10-28",
  },
  {
    id: 3,
    number: "ORD-2025-003",
    customer: "Carlos Pereira",
    status: "SHIPPED",
    items: 2,
    total: 2790.0,
    createdAt: "2025-10-22",
  },
  {
    id: 4,
    number: "ORD-2025-004",
    customer: "Ana Lima",
    status: "CANCELLED",
    items: 1,
    total: 999.0,
    createdAt: "2025-10-15",
  },
];

const statusMap = {
  PENDING: { label: "Pendente", variant: "secondary" as const },
  PAID: { label: "Pago", variant: "default" as const },
  SHIPPED: { label: "Enviado", variant: "outline" as const },
  CANCELLED: { label: "Cancelado", variant: "destructive" as const },
};

export default function Orders() {
  const [orders, setOrders] = useState(mockSalesOrders);
  const [statusFilter, setStatusFilter] = useState<string>("all");
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const filteredOrders = orders.filter(
    (order) => statusFilter === "all" || order.status === statusFilter
  );

  const handleCreateOrder = (newOrder: any) => {
    setOrders((prev) => [...prev, newOrder]);
  };

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">
            Pedidos de Clientes
          </h2>
          <p className="text-muted-foreground">Gerencie pedidos de venda</p>
        </div>

        <Button onClick={() => setIsDialogOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Novo Pedido
        </Button>
      </div>

      {/* Modal de novo pedido */}
      <NewOrderDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        onCreate={handleCreateOrder}
      />

      <Card>
        <CardHeader>
          <CardTitle>Lista de Pedidos</CardTitle>
          <CardDescription>
            Visualize e gerencie todos os pedidos de clientes
          </CardDescription>
        </CardHeader>
        <CardContent>
          <div className="mb-4">
            <Select value={statusFilter} onValueChange={setStatusFilter}>
              <SelectTrigger className="w-[200px]">
                <SelectValue placeholder="Status" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="all">Todos</SelectItem>
                <SelectItem value="PENDING">Pendente</SelectItem>
                <SelectItem value="PAID">Pago</SelectItem>
                <SelectItem value="SHIPPED">Enviado</SelectItem>
                <SelectItem value="CANCELLED">Cancelado</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Número</TableHead>
                <TableHead>Cliente</TableHead>
                <TableHead>Status</TableHead>
                <TableHead className="text-right">Itens</TableHead>
                <TableHead className="text-right">Total</TableHead>
                <TableHead>Data</TableHead>
                <TableHead className="text-right">Ações</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {filteredOrders.map((order) => (
                <TableRow key={order.id}>
                  <TableCell className="font-medium">{order.number}</TableCell>
                  <TableCell>{order.customer}</TableCell>
                  <TableCell>
                    <Badge
                      variant={
                        statusMap[order.status as keyof typeof statusMap]
                          .variant
                      }
                    >
                      {statusMap[order.status as keyof typeof statusMap].label}
                    </Badge>
                  </TableCell>
                  <TableCell className="text-right">{order.items}</TableCell>
                  <TableCell className="text-right">
                    R${" "}
                    {order.total.toLocaleString("pt-BR", {
                      minimumFractionDigits: 2,
                    })}
                  </TableCell>
                  <TableCell>{order.createdAt}</TableCell>
                  <TableCell className="text-right">
                    <DropdownMenu>
                      <DropdownMenuTrigger asChild>
                        <Button variant="ghost" size="icon">
                          <MoreHorizontal className="h-4 w-4" />
                        </Button>
                      </DropdownMenuTrigger>
                      <DropdownMenuContent align="end">
                        <DropdownMenuLabel>Ações</DropdownMenuLabel>
                        <DropdownMenuSeparator />
                        <DropdownMenuItem>
                          <Eye className="mr-2 h-4 w-4" />
                          Ver Detalhes
                        </DropdownMenuItem>
                        {order.status === "PENDING" && (
                          <DropdownMenuItem>
                            <Check className="mr-2 h-4 w-4" />
                            Marcar como Pago
                          </DropdownMenuItem>
                        )}
                        {order.status !== "CANCELLED" && (
                          <DropdownMenuItem className="text-destructive">
                            <X className="mr-2 h-4 w-4" />
                            Cancelar Pedido
                          </DropdownMenuItem>
                        )}
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </div>
  );
}
