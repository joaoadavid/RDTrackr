import { useState, useEffect } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@/components/ui/select";

interface EditItemDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  item: any;
  onUpdate: (item: any) => void;
}

export function EditItemDialog({
  open,
  onOpenChange,
  item,
  onUpdate,
}: EditItemDialogProps) {
  const [form, setForm] = useState(item);

  useEffect(() => {
    setForm(item);
  }, [item]);

  if (!form) return null;

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const updated = {
      ...form,
      price: parseFloat(form.price),
      stock: parseInt(form.stock),
      reorderPoint: parseInt(form.reorderPoint),
      updatedAt: new Date().toISOString().split("T")[0],
    };
    onUpdate(updated);
    onOpenChange(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-md">
        <DialogHeader>
          <DialogTitle>Editar Item</DialogTitle>
          <DialogDescription>
            Atualize as informações do produto selecionado.
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label htmlFor="sku">SKU</Label>
              <Input
                id="sku"
                value={form.sku}
                onChange={(e) => setForm({ ...form, sku: e.target.value })}
              />
            </div>
            <div>
              <Label htmlFor="name">Nome</Label>
              <Input
                id="name"
                value={form.name}
                onChange={(e) => setForm({ ...form, name: e.target.value })}
              />
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label htmlFor="category">Categoria</Label>
              <Input
                id="category"
                value={form.category}
                onChange={(e) => setForm({ ...form, category: e.target.value })}
              />
            </div>
            <div>
              <Label htmlFor="uom">Unidade</Label>
              <Select
                value={form.uom}
                onValueChange={(val) => setForm({ ...form, uom: val })}
              >
                <SelectTrigger id="uom">
                  <SelectValue placeholder="Unidade" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="UN">Unidade</SelectItem>
                  <SelectItem value="KG">Kg</SelectItem>
                  <SelectItem value="CX">Caixa</SelectItem>
                </SelectContent>
              </Select>
            </div>
          </div>

          <div className="grid grid-cols-3 gap-4">
            <div>
              <Label htmlFor="price">Preço</Label>
              <Input
                id="price"
                type="number"
                value={form.price}
                onChange={(e) => setForm({ ...form, price: e.target.value })}
              />
            </div>
            <div>
              <Label htmlFor="stock">Estoque</Label>
              <Input
                id="stock"
                type="number"
                value={form.stock}
                onChange={(e) => setForm({ ...form, stock: e.target.value })}
              />
            </div>
            <div>
              <Label htmlFor="reorderPoint">Reorder</Label>
              <Input
                id="reorderPoint"
                type="number"
                value={form.reorderPoint}
                onChange={(e) =>
                  setForm({ ...form, reorderPoint: e.target.value })
                }
              />
            </div>
          </div>

          <DialogFooter>
            <Button type="submit">Salvar Alterações</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
