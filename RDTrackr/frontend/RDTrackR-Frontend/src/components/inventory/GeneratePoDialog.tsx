import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Checkbox } from "@/components/ui/checkbox";
import { mockSuppliers } from "@/lib/mock-data";
import { ReplenishmentItem } from "./ReplenishmentTable";

interface GeneratePoDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  items: ReplenishmentItem[];
  onConfirm: (supplierId: string, notes: string, groupBySupplier: boolean) => void;
}

export function GeneratePoDialog({
  open,
  onOpenChange,
  items,
  onConfirm,
}: GeneratePoDialogProps) {
  const [supplierId, setSupplierId] = useState("");
  const [notes, setNotes] = useState("");
  const [groupBySupplier, setGroupBySupplier] = useState(false);

  const totalValue = items.reduce((sum, item) => sum + item.suggestedQty * item.unitPrice, 0);

  const handleConfirm = () => {
    if (!supplierId) {
      return;
    }
    onConfirm(supplierId, notes, groupBySupplier);
    // Reset
    setSupplierId("");
    setNotes("");
    setGroupBySupplier(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-2xl">
        <DialogHeader>
          <DialogTitle>Gerar Pedido de Compra</DialogTitle>
          <DialogDescription>
            Você está prestes a gerar um rascunho de pedido de compra com {items.length} item(ns).
          </DialogDescription>
        </DialogHeader>

        <div className="space-y-4 py-4">
          {/* Resumo dos itens */}
          <div className="rounded-lg border p-4 space-y-2">
            <h4 className="font-semibold text-sm">Resumo</h4>
            <div className="space-y-1 text-sm">
              {items.map((item) => (
                <div key={item.id} className="flex justify-between">
                  <span className="text-muted-foreground">
                    {item.sku} - {item.name}
                  </span>
                  <span className="font-medium">
                    {item.suggestedQty} {item.uom} × R$ {item.unitPrice.toFixed(2)} = R${" "}
                    {(item.suggestedQty * item.unitPrice).toFixed(2)}
                  </span>
                </div>
              ))}
              <div className="flex justify-between pt-2 border-t font-semibold">
                <span>Total estimado</span>
                <span>R$ {totalValue.toFixed(2)}</span>
              </div>
            </div>
          </div>

          {/* Fornecedor */}
          <div className="space-y-2">
            <Label htmlFor="supplier">Fornecedor *</Label>
            <Select value={supplierId} onValueChange={setSupplierId}>
              <SelectTrigger id="supplier">
                <SelectValue placeholder="Selecione o fornecedor" />
              </SelectTrigger>
              <SelectContent>
                {mockSuppliers.map((supplier) => (
                  <SelectItem key={supplier.id} value={supplier.id}>
                    {supplier.name} ({supplier.email})
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          {/* Agrupar por fornecedor */}
          <div className="flex items-center space-x-2">
            <Checkbox
              id="group"
              checked={groupBySupplier}
              onCheckedChange={(checked) => setGroupBySupplier(checked as boolean)}
            />
            <Label htmlFor="group" className="text-sm cursor-pointer">
              Agrupar itens por fornecedor preferencial (quando aplicável)
            </Label>
          </div>

          {/* Observações */}
          <div className="space-y-2">
            <Label htmlFor="notes">Observações</Label>
            <Textarea
              id="notes"
              placeholder="Informações adicionais para o pedido..."
              value={notes}
              onChange={(e) => setNotes(e.target.value)}
              rows={3}
            />
          </div>
        </div>

        <DialogFooter>
          <Button variant="outline" onClick={() => onOpenChange(false)}>
            Cancelar
          </Button>
          <Button onClick={handleConfirm} disabled={!supplierId}>
            Gerar Pedido (Rascunho)
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
