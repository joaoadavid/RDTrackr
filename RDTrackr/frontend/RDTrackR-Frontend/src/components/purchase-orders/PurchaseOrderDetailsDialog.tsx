import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { format } from "date-fns";
import { ptBR } from "date-fns/locale";

interface PurchaseOrderDetailsDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  order: any;
}

// 🔹 Itens de pedido mockados (simulação)
const mockItems = [
  { id: 1, product: "Notebook Dell Inspiron 15", qty: 2, price: 3499.0 },
  { id: 2, product: "Mouse Logitech M170", qty: 3, price: 129.0 },
  { id: 3, product: 'Monitor LG Ultrawide 29"', qty: 1, price: 1299.0 },
];

export function PurchaseOrderDetailsDialog({
  open,
  onOpenChange,
  order,
}: PurchaseOrderDetailsDialogProps) {
  if (!order) return null;

  const formattedDate = format(
    new Date(order.createdAt),
    "dd 'de' MMMM 'de' yyyy",
    {
      locale: ptBR,
    }
  );

  // 🔸 Calcula subtotal e total simulados
  const subtotal = mockItems.reduce(
    (acc, item) => acc + item.price * item.qty,
    0
  );
  const taxes = subtotal * 0.12; // imposto simulado
  const total = subtotal + taxes;

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-2xl">
        <DialogHeader>
          <DialogTitle>Detalhes do Pedido</DialogTitle>
          <DialogDescription>
            Informações completas sobre o pedido de compra selecionado.
          </DialogDescription>
        </DialogHeader>

        <div className="space-y-6 py-2">
          {/* 📋 Informações gerais */}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <p className="text-sm text-muted-foreground">Número do Pedido</p>
              <p className="font-semibold">{order.number}</p>
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Data</p>
              <p className="font-semibold">{formattedDate}</p>
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Fornecedor</p>
              <p className="font-semibold">{order.supplier}</p>
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Status</p>
              <Badge
                variant={
                  order.status === "CANCELLED"
                    ? "destructive"
                    : order.status === "PENDING"
                    ? "secondary"
                    : order.status === "DRAFT"
                    ? "outline"
                    : "default"
                }
              >
                {order.status === "CANCELLED"
                  ? "Cancelado"
                  : order.status === "PENDING"
                  ? "Pendente"
                  : order.status === "DRAFT"
                  ? "Rascunho"
                  : "Aprovado"}
              </Badge>
            </div>
          </div>

          <Separator />

          {/* 🧾 Tabela de itens */}
          <div>
            <p className="text-sm font-semibold mb-2">Itens do Pedido</p>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Produto</TableHead>
                  <TableHead className="text-right">Qtd.</TableHead>
                  <TableHead className="text-right">Preço Unitário</TableHead>
                  <TableHead className="text-right">Subtotal</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {mockItems.map((item) => (
                  <TableRow key={item.id}>
                    <TableCell>{item.product}</TableCell>
                    <TableCell className="text-right">{item.qty}</TableCell>
                    <TableCell className="text-right">
                      R${" "}
                      {item.price.toLocaleString("pt-BR", {
                        minimumFractionDigits: 2,
                      })}
                    </TableCell>
                    <TableCell className="text-right">
                      R${" "}
                      {(item.qty * item.price).toLocaleString("pt-BR", {
                        minimumFractionDigits: 2,
                      })}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </div>

          {/* 💰 Totais */}
          <div className="flex justify-end mt-4">
            <div className="w-1/2 space-y-1">
              <div className="flex justify-between">
                <span className="text-sm text-muted-foreground">Subtotal</span>
                <span className="font-medium">
                  R${" "}
                  {subtotal.toLocaleString("pt-BR", {
                    minimumFractionDigits: 2,
                  })}
                </span>
              </div>
              <div className="flex justify-between">
                <span className="text-sm text-muted-foreground">
                  Impostos (12%)
                </span>
                <span className="font-medium">
                  R${" "}
                  {taxes.toLocaleString("pt-BR", { minimumFractionDigits: 2 })}
                </span>
              </div>
              <Separator />
              <div className="flex justify-between">
                <span className="text-sm font-semibold">Total</span>
                <span className="text-lg font-bold text-primary">
                  R${" "}
                  {total.toLocaleString("pt-BR", { minimumFractionDigits: 2 })}
                </span>
              </div>
            </div>
          </div>
        </div>

        <DialogFooter>
          <Button variant="outline" onClick={() => onOpenChange(false)}>
            Fechar
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
