import { useState } from "react";
import {
  Plus,
  MoreHorizontal,
  Pencil,
  Trash2,
  ArrowRightLeft,
} from "lucide-react";
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
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { useToast } from "@/hooks/use-toast";
import { NewWarehouseDialog } from "@/components/inventory/NewWarehouseDialog";

// Mock inicial
const mockWarehouses = [
  {
    id: 1,
    name: "Depósito Principal",
    location: "São Paulo - SP",
    capacity: "10.000",
    items: 1247,
    utilization: 78,
    createdAt: "2024-01-15",
  },
  {
    id: 2,
    name: "CD Rio de Janeiro",
    location: "Rio de Janeiro - RJ",
    capacity: "5.000",
    items: 623,
    utilization: 62,
    createdAt: "2024-03-20",
  },
  {
    id: 3,
    name: "Depósito Nordeste",
    location: "Recife - PE",
    capacity: "3.500",
    items: 412,
    utilization: 59,
    createdAt: "2024-06-10",
  },
  {
    id: 4,
    name: "CD Sul",
    location: "Porto Alegre - RS",
    capacity: "4.200",
    items: 534,
    utilization: 64,
    createdAt: "2024-08-05",
  },
];

export default function Warehouses() {
  const { toast } = useToast();
  const [warehouses, setWarehouses] = useState(mockWarehouses);
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const getUtilizationColor = (utilization: number) => {
    if (utilization >= 80) return "destructive";
    if (utilization >= 60) return "secondary";
    return "default";
  };

  const handleAddWarehouse = (warehouse: any) => {
    setWarehouses((prev) => [...prev, warehouse]);
    toast({
      title: "Depósito adicionado",
      description: `O depósito "${warehouse.name}" foi criado com sucesso.`,
    });
  };

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">Depósitos</h2>
          <p className="text-muted-foreground">
            Gerencie locais de armazenamento
          </p>
        </div>
        <Button onClick={() => setIsDialogOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Adicionar Depósito
        </Button>
      </div>

      {/* Modal */}
      <NewWarehouseDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        onCreate={handleAddWarehouse}
      />

      <Card>
        <CardHeader>
          <CardTitle>Lista de Depósitos</CardTitle>
          <CardDescription>
            Visualize e gerencie todos os locais de estoque
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Nome</TableHead>
                <TableHead>Localização</TableHead>
                <TableHead>Capacidade</TableHead>
                <TableHead className="text-right">Itens</TableHead>
                <TableHead>Utilização</TableHead>
                <TableHead>Criado em</TableHead>
                <TableHead className="text-right">Ações</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {warehouses.map((warehouse) => (
                <TableRow key={warehouse.id}>
                  <TableCell className="font-medium">
                    {warehouse.name}
                  </TableCell>
                  <TableCell>{warehouse.location}</TableCell>
                  <TableCell>{warehouse.capacity}</TableCell>
                  <TableCell className="text-right">
                    {warehouse.items}
                  </TableCell>
                  <TableCell>
                    <div className="flex items-center gap-2">
                      <Badge
                        variant={getUtilizationColor(warehouse.utilization)}
                      >
                        {warehouse.utilization}%
                      </Badge>
                    </div>
                  </TableCell>
                  <TableCell>{warehouse.createdAt}</TableCell>
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
                        <DropdownMenuItem>
                          <ArrowRightLeft className="mr-2 h-4 w-4" />
                          Transferir
                        </DropdownMenuItem>
                        <DropdownMenuItem>
                          <Pencil className="mr-2 h-4 w-4" />
                          Editar
                        </DropdownMenuItem>
                        <DropdownMenuItem className="text-destructive">
                          <Trash2 className="mr-2 h-4 w-4" />
                          Excluir
                        </DropdownMenuItem>
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
