import { useState } from "react";
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
import { Badge } from "@/components/ui/badge";
import { useToast } from "@/hooks/use-toast";

interface NewWarehouseDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCreate: (warehouse: any) => void;
}

export function NewWarehouseDialog({
  open,
  onOpenChange,
  onCreate,
}: NewWarehouseDialogProps) {
  const { toast } = useToast();

  const [form, setForm] = useState({
    name: "",
    location: "",
    capacity: "",
    items: "",
  });

  // Função auxiliar para cálculo automático
  const calcUtilization = () => {
    const capacity = parseFloat(form.capacity) || 0;
    const items = parseInt(form.items) || 0;
    if (capacity === 0) return 0;

    const value = (items / capacity) * 100;
    const clamped = Math.min(Math.max(value, 0), 100); // garante 0-100%

    return parseFloat(clamped.toFixed(2)); // limita a 2 casas decimais
  };

  const utilization = calcUtilization();

  const getUtilizationColor = (utilization: number) => {
    if (utilization >= 80) return "destructive";
    if (utilization >= 60) return "secondary";
    return "default";
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!form.name || !form.location || !form.capacity) {
      toast({
        title: "Campos obrigatórios",
        description: "Informe nome, localização e capacidade do depósito.",
        variant: "destructive",
      });
      return;
    }

    const newWarehouse = {
      id: Date.now(),
      name: form.name,
      location: form.location,
      capacity: `${form.capacity}`,
      items: parseInt(form.items) || 0,
      utilization: utilization,
      createdAt: new Date().toISOString().split("T")[0],
    };

    onCreate(newWarehouse);
    toast({
      title: "Depósito adicionado",
      description: `O depósito "${form.name}" foi criado com ${
        form.items || 0
      } itens.`,
    });

    // Resetar formulário
    setForm({ name: "", location: "", capacity: "", items: "" });
    onOpenChange(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-md">
        <DialogHeader>
          <DialogTitle>Novo Depósito</DialogTitle>
          <DialogDescription>
            Cadastre um novo local de armazenamento e acompanhe a capacidade
            utilizada.
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <Label>Nome do Depósito</Label>
            <Input
              value={form.name}
              onChange={(e) => setForm({ ...form, name: e.target.value })}
              placeholder="Ex: Depósito Principal"
              required
            />
          </div>

          <div>
            <Label>Localização</Label>
            <Input
              value={form.location}
              onChange={(e) => setForm({ ...form, location: e.target.value })}
              placeholder="Ex: São Paulo - SP"
              required
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label>Capacidade Total (Itens)</Label>
              <Input
                type="number"
                value={form.capacity}
                onChange={(e) => setForm({ ...form, capacity: e.target.value })}
                placeholder="Ex: 10000"
                required
              />
            </div>
            <div>
              <Label>Itens Armazenados</Label>
              <Input
                type="number"
                value={form.items}
                onChange={(e) => setForm({ ...form, items: e.target.value })}
                placeholder="Ex: 500"
              />
            </div>
          </div>

          <div className="flex items-center justify-between mt-2">
            <Label>Capacidade Utilizada</Label>
            <Badge variant={getUtilizationColor(utilization)}>
              {utilization.toFixed(1)}%
            </Badge>
          </div>

          <DialogFooter>
            <Button type="submit">Salvar Depósito</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
