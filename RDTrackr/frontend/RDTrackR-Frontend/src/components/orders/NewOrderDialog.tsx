import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

interface NewOrderDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCreate: (order: any) => void;
}

export function NewOrderDialog({
  open,
  onOpenChange,
  onCreate,
}: NewOrderDialogProps) {
  const [form, setForm] = useState({ customer: "", items: "", total: "" });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const newOrder = {
      id: Date.now(),
      number: `ORD-${Date.now()}`,
      customer: form.customer,
      status: "PENDING",
      items: Number(form.items),
      total: Number(form.total),
      createdAt: new Date().toISOString().split("T")[0],
    };

    onCreate(newOrder);
    onOpenChange(false);
    setForm({ customer: "", items: "", total: "" });
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-lg">
        <DialogHeader>
          <DialogTitle>Novo Pedido</DialogTitle>
        </DialogHeader>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="customer">Cliente</Label>
            <Input
              id="customer"
              value={form.customer}
              onChange={(e) => setForm({ ...form, customer: e.target.value })}
              placeholder="Nome do cliente"
              required
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="items">Itens</Label>
            <Input
              id="items"
              type="number"
              value={form.items}
              onChange={(e) => setForm({ ...form, items: e.target.value })}
              placeholder="Quantidade"
              required
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="total">Valor total (R$)</Label>
            <Input
              id="total"
              type="number"
              value={form.total}
              onChange={(e) => setForm({ ...form, total: e.target.value })}
              placeholder="0,00"
              required
            />
          </div>

          <DialogFooter>
            <Button type="submit" className="w-full">
              Salvar Pedido
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
