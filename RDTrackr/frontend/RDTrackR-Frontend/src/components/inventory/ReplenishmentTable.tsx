import { useState } from "react";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Card } from "@/components/ui/card";

export interface ReplenishmentItem {
  id: string;
  sku: string;
  name: string;
  category: string;
  uom: string;
  currentStock: number;
  reorderPoint: number;
  dailyConsumption: number;
  leadTime: number;
  suggestedQty: number;
  isCritical: boolean;
  unitPrice: number;
}

interface ReplenishmentTableProps {
  items: ReplenishmentItem[];
  selectedIds: Set<string>;
  onToggleItem: (id: string) => void;
  onToggleAll: () => void;
  onSelectCritical: () => void;
  onQtyChange: (id: string, qty: number) => void;
}

export function ReplenishmentTable({
  items,
  selectedIds,
  onToggleItem,
  onToggleAll,
  onSelectCritical,
  onQtyChange,
}: ReplenishmentTableProps) {
  const allSelected = items.length > 0 && items.every((item) => selectedIds.has(item.id));

  if (items.length === 0) {
    return (
      <Card className="p-8 text-center">
        <p className="text-muted-foreground">Nenhum produto encontrado com os filtros selecionados.</p>
      </Card>
    );
  }

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <Button variant="outline" onClick={onSelectCritical}>
          Selecionar itens críticos
        </Button>
        <p className="text-sm text-muted-foreground">
          {selectedIds.size} item(ns) selecionado(s)
        </p>
      </div>

      <Card className="overflow-hidden">
        <div className="overflow-x-auto">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead className="w-12">
                  <Checkbox
                    checked={allSelected}
                    onCheckedChange={onToggleAll}
                    aria-label="Selecionar todos"
                  />
                </TableHead>
                <TableHead>SKU</TableHead>
                <TableHead>Produto</TableHead>
                <TableHead>Categoria</TableHead>
                <TableHead>UoM</TableHead>
                <TableHead className="text-right">Estoque</TableHead>
                <TableHead className="text-right">ROP</TableHead>
                <TableHead className="text-right">Consumo/dia</TableHead>
                <TableHead className="text-right">Lead Time</TableHead>
                <TableHead className="text-right">Sugestão</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {items.map((item) => (
                <TableRow
                  key={item.id}
                  className={item.isCritical ? "bg-destructive/5" : ""}
                >
                  <TableCell>
                    <Checkbox
                      checked={selectedIds.has(item.id)}
                      onCheckedChange={() => onToggleItem(item.id)}
                      aria-label={`Selecionar ${item.name}`}
                    />
                  </TableCell>
                  <TableCell className="font-mono text-sm">{item.sku}</TableCell>
                  <TableCell className="font-medium">{item.name}</TableCell>
                  <TableCell>
                    <span className="text-xs px-2 py-1 rounded-full bg-muted">
                      {item.category}
                    </span>
                  </TableCell>
                  <TableCell>{item.uom}</TableCell>
                  <TableCell className="text-right">
                    <span className={item.isCritical ? "text-destructive font-semibold" : ""}>
                      {item.currentStock.toFixed(1)}
                    </span>
                  </TableCell>
                  <TableCell className="text-right">{item.reorderPoint.toFixed(1)}</TableCell>
                  <TableCell className="text-right">{item.dailyConsumption.toFixed(2)}</TableCell>
                  <TableCell className="text-right">{item.leadTime}d</TableCell>
                  <TableCell className="text-right">
                    <Input
                      type="number"
                      min="0"
                      step="0.1"
                      value={item.suggestedQty}
                      onChange={(e) => onQtyChange(item.id, Number(e.target.value))}
                      className="w-24 text-right"
                    />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      </Card>
    </div>
  );
}
