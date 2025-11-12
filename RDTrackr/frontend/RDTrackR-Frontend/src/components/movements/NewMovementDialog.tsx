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

interface NewMovementDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCreate: (movement: any) => void;
}

export function NewMovementDialog({
  open,
  onOpenChange,
  onCreate,
}: NewMovementDialogProps) {
  const [form, setForm] = useState({
    type: "INBOUND",
    reference: "",
    product: "",
    warehouse: "",
    quantity: 0,
    user: "Admin",
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const now = new Date().toLocaleString("pt-BR");

    const newMovement = {
      id: Date.now(),
      ...form,
      quantity:
        form.type === "OUTBOUND"
          ? -Math.abs(form.quantity)
          : Math.abs(form.quantity),
      createdAt: now,
    };

    onCreate(newMovement);
    setForm({
      type: "INBOUND",
      reference: "",
      product: "",
      warehouse: "",
      quantity: 0,
      user: "Admin",
    });
    onOpenChange(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-md">
        <DialogHeader>
          <DialogTitle>Nova Movimentação</DialogTitle>
          <DialogDescription>
            Registre uma entrada, saída ou ajuste de estoque.
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <Label htmlFor="type">Tipo</Label>
            <Select
              value={form.type}
              onValueChange={(v) => setForm({ ...form, type: v })}
            >
              <SelectTrigger id="type">
                <SelectValue placeholder="Selecione o tipo" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="INBOUND">Entrada</SelectItem>
                <SelectItem value="OUTBOUND">Saída</SelectItem>
                <SelectItem value="ADJUST">Ajuste</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <div>
            <Label htmlFor="reference">Referência</Label>
            <Input
              id="reference"
              placeholder="Ex: PO-010"
              value={form.reference}
              onChange={(e) => setForm({ ...form, reference: e.target.value })}
              required
            />
          </div>

          <div>
            <Label htmlFor="product">Produto</Label>
            <Input
              id="product"
              placeholder="Ex: Monitor LG"
              value={form.product}
              onChange={(e) => setForm({ ...form, product: e.target.value })}
              required
            />
          </div>

          <div>
            <Label htmlFor="warehouse">Depósito</Label>
            <Input
              id="warehouse"
              placeholder="Ex: CD São Paulo"
              value={form.warehouse}
              onChange={(e) => setForm({ ...form, warehouse: e.target.value })}
              required
            />
          </div>

          <div>
            <Label htmlFor="quantity">Quantidade</Label>
            <Input
              id="quantity"
              type="number"
              placeholder="Ex: 25"
              value={form.quantity}
              onChange={(e) =>
                setForm({ ...form, quantity: Number(e.target.value) })
              }
              required
            />
          </div>

          <DialogFooter>
            <Button type="submit">Salvar Movimentação</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
