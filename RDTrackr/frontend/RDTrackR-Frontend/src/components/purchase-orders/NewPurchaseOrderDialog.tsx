import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

interface NewPurchaseOrderDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCreate: (order: any) => void;
}

export function NewPurchaseOrderDialog({
  open,
  onOpenChange,
  onCreate,
}: NewPurchaseOrderDialogProps) {
  const [form, setForm] = useState({
    supplier: "",
    items: 1,
    total: 0,
    status: "DRAFT",
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const now = new Date().toISOString().split("T")[0];
    const number = `PO-${now.replace(/-/g, "").slice(2)}-${Math.floor(
      Math.random() * 100
    )
      .toString()
      .padStart(3, "0")}`;

    const newOrder = {
      id: Date.now(),
      number,
      createdAt: now,
      ...form,
    };

    onCreate(newOrder);
    setForm({ supplier: "", items: 1, total: 0, status: "DRAFT" });
    onOpenChange(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-md">
        <DialogHeader>
          <DialogTitle>Novo Pedido de Compra</DialogTitle>
          <DialogDescription>
            Crie um novo pedido de compra e defina o fornecedor, itens e valor
            total.
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <Label>Fornecedor</Label>
            <Input
              placeholder="Ex: TechSupply Brasil"
              value={form.supplier}
              onChange={(e) => setForm({ ...form, supplier: e.target.value })}
              required
            />
          </div>

          <div>
            <Label>Quantidade de Itens</Label>
            <Input
              type="number"
              min={1}
              value={form.items}
              onChange={(e) =>
                setForm({ ...form, items: Number(e.target.value) })
              }
              required
            />
          </div>

          <div>
            <Label>Valor Total (R$)</Label>
            <Input
              type="number"
              step="0.01"
              value={form.total}
              onChange={(e) =>
                setForm({ ...form, total: parseFloat(e.target.value) })
              }
              required
            />
          </div>

          <div>
            <Label>Status</Label>
            <Select
              value={form.status}
              onValueChange={(v) => setForm({ ...form, status: v })}
            >
              <SelectTrigger>
                <SelectValue placeholder="Selecione o status" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="DRAFT">Rascunho</SelectItem>
                <SelectItem value="PENDING">Pendente</SelectItem>
                <SelectItem value="APPROVED">Aprovado</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <DialogFooter>
            <Button type="submit" data-testid="submit">
              Salvar Pedido
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
