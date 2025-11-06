import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Badge } from "@/components/ui/badge";

const orders = [
  { id: "#1234", customer: "João Silva", status: "completed", total: "R$ 1.234,00", date: "2025-10-29" },
  { id: "#1235", customer: "Maria Santos", status: "pending", total: "R$ 856,00", date: "2025-10-29" },
  { id: "#1236", customer: "Pedro Costa", status: "processing", total: "R$ 2.145,00", date: "2025-10-28" },
  { id: "#1237", customer: "Ana Oliveira", status: "completed", total: "R$ 645,00", date: "2025-10-28" },
  { id: "#1238", customer: "Carlos Souza", status: "cancelled", total: "R$ 1.890,00", date: "2025-10-27" },
];

const statusMap = {
  completed: { label: "Concluído", variant: "default" as const },
  pending: { label: "Pendente", variant: "secondary" as const },
  processing: { label: "Processando", variant: "outline" as const },
  cancelled: { label: "Cancelado", variant: "destructive" as const },
};

export function RecentOrdersTable() {
  return (
    <Card className="col-span-2">
      <CardHeader>
        <CardTitle>Pedidos Recentes</CardTitle>
        <CardDescription>Últimos 5 pedidos realizados</CardDescription>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Pedido</TableHead>
              <TableHead>Cliente</TableHead>
              <TableHead>Status</TableHead>
              <TableHead>Data</TableHead>
              <TableHead className="text-right">Total</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {orders.map((order) => (
              <TableRow key={order.id}>
                <TableCell className="font-medium">{order.id}</TableCell>
                <TableCell>{order.customer}</TableCell>
                <TableCell>
                  <Badge variant={statusMap[order.status as keyof typeof statusMap].variant}>
                    {statusMap[order.status as keyof typeof statusMap].label}
                  </Badge>
                </TableCell>
                <TableCell>{order.date}</TableCell>
                <TableCell className="text-right">{order.total}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  );
}
