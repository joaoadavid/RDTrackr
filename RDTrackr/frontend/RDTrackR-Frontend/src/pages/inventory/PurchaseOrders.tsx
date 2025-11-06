import { useState } from "react";
import { Plus, MoreHorizontal, Eye, Check, X } from "lucide-react";
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
import { useToast } from "@/hooks/use-toast";
import { NewPurchaseOrderDialog } from "@/components/purchase-orders/NewPurchaseOrderDialog";
import { PurchaseOrderDetailsDialog } from "@/components/purchase-orders/PurchaseOrderDetailsDialog";

const mockPurchaseOrders = [
  {
    id: 1,
    number: "PO-2025-001",
    supplier: "TechSupply Brasil",
    status: "APPROVED",
    items: 5,
    total: 25490.0,
    createdAt: "2025-10-25",
  },
  {
    id: 2,
    number: "PO-2025-002",
    supplier: "InfoParts Distribuidora",
    status: "PENDING",
    items: 3,
    total: 12350.0,
    createdAt: "2025-10-28",
  },
  {
    id: 3,
    number: "PO-2025-003",
    supplier: "GlobalTech Solutions",
    status: "RECEIVED",
    items: 8,
    total: 45780.0,
    createdAt: "2025-10-20",
  },
  {
    id: 4,
    number: "PO-2025-004",
    supplier: "CompuMax Imports",
    status: "DRAFT",
    items: 2,
    total: 8900.0,
    createdAt: "2025-10-30",
  },
  {
    id: 5,
    number: "PO-2025-005",
    supplier: "TechSupply Brasil",
    status: "CANCELLED",
    items: 4,
    total: 18650.0,
    createdAt: "2025-10-15",
  },
];

const statusMap = {
  DRAFT: { label: "Rascunho", variant: "outline" as const },
  PENDING: { label: "Pendente", variant: "secondary" as const },
  APPROVED: { label: "Aprovado", variant: "default" as const },
  RECEIVED: { label: "Recebido", variant: "default" as const },
  CANCELLED: { label: "Cancelado", variant: "destructive" as const },
};

export default function PurchaseOrders() {
  const [statusFilter, setStatusFilter] = useState<string>("all");
  const [orders, setOrders] = useState(mockPurchaseOrders);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [isDetailsOpen, setIsDetailsOpen] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState<any | null>(null);
  const { toast } = useToast();

  const filteredOrders = orders.filter(
    (order) => statusFilter === "all" || order.status === statusFilter
  );

  const handleAddOrder = (newOrder: any) => {
    setOrders((prev) => [...prev, newOrder]);
    toast({
      title: "Pedido criado com sucesso",
      description: `O pedido ${newOrder.number} foi adicionado à lista.`,
    });
  };

  const handleUpdateStatus = (id: number, newStatus: string) => {
    setOrders((prev) =>
      prev.map((order) =>
        order.id === id ? { ...order, status: newStatus } : order
      )
    );
  };

  const handleDuplicateOrder = (newOrder: any) => {
    setOrders((prev) => [...prev, newOrder]);
  };

  return (
    <div className="space-y-6">
      {/* Cabeçalho e botão */}
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">
            Pedidos de Compra
          </h2>
          <p className="text-muted-foreground">
            Gerencie pedidos de reposição de estoque
          </p>
        </div>
        <Button onClick={() => setIsDialogOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Novo Pedido
        </Button>
      </div>

      {/* Modal de Novo Pedido */}
      <NewPurchaseOrderDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        onCreate={handleAddOrder}
      />

      {/* Modal de Detalhes */}
      <PurchaseOrderDetailsDialog
        open={isDetailsOpen}
        onOpenChange={setIsDetailsOpen}
        order={selectedOrder}
        onUpdateStatus={handleUpdateStatus}
        onDuplicate={handleDuplicateOrder}
      />

      {/* Tabela principal */}
      <Card>
        <CardHeader>
          <CardTitle>Lista de Pedidos de Compra</CardTitle>
          <CardDescription>
            Visualize e gerencie todos os pedidos aos fornecedores
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
                <SelectItem value="DRAFT">Rascunho</SelectItem>
                <SelectItem value="PENDING">Pendente</SelectItem>
                <SelectItem value="APPROVED">Aprovado</SelectItem>
                <SelectItem value="RECEIVED">Recebido</SelectItem>
                <SelectItem value="CANCELLED">Cancelado</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Número</TableHead>
                <TableHead>Fornecedor</TableHead>
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
                  <TableCell>{order.supplier}</TableCell>
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

                        {/* Ver Detalhes */}
                        <DropdownMenuItem
                          onClick={() => {
                            setSelectedOrder(order);
                            setIsDetailsOpen(true);
                          }}
                        >
                          <Eye className="mr-2 h-4 w-4" />
                          Ver Detalhes
                        </DropdownMenuItem>

                        {/* Receber Pedido */}
                        {order.status === "APPROVED" && (
                          <DropdownMenuItem
                            onClick={() =>
                              handleUpdateStatus(order.id, "RECEIVED")
                            }
                          >
                            <Check className="mr-2 h-4 w-4" />
                            Receber Pedido
                          </DropdownMenuItem>
                        )}

                        {/* Cancelar Pedido */}
                        {(order.status === "DRAFT" ||
                          order.status === "PENDING") && (
                          <DropdownMenuItem
                            className="text-destructive"
                            onClick={() =>
                              handleUpdateStatus(order.id, "CANCELLED")
                            }
                          >
                            <X className="mr-2 h-4 w-4" />
                            Cancelar
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
