import { useState } from "react";
import { Search, Plus, MoreHorizontal, Pencil, Trash2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
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
import { useToast } from "@/hooks/use-toast";
import { NewSupplierDialog } from "@/components/suppliers/NewSupplierDialog";
import { EditSupplierDialog } from "@/components/suppliers/EditSupplierDialog";

const mockSuppliers = [
  {
    id: 1,
    name: "TechSupply Brasil",
    contact: "Carlos Silva",
    email: "carlos@techsupply.com",
    phone: "(11) 3456-7890",
    address: "São Paulo - SP",
  },
  {
    id: 2,
    name: "InfoParts Distribuidora",
    contact: "Ana Santos",
    email: "ana@infoparts.com",
    phone: "(21) 2345-6789",
    address: "Rio de Janeiro - RJ",
  },
  {
    id: 3,
    name: "CompuMax Imports",
    contact: "Roberto Costa",
    email: "roberto@compumax.com",
    phone: "(41) 4567-8901",
    address: "Curitiba - PR",
  },
  {
    id: 4,
    name: "GlobalTech Solutions",
    contact: "Mariana Oliveira",
    email: "mariana@globaltech.com",
    phone: "(11) 5678-9012",
    address: "São Paulo - SP",
  },
];

export default function Suppliers() {
  const { toast } = useToast();
  const [search, setSearch] = useState("");
  const [suppliers, setSuppliers] = useState(mockSuppliers);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false);
  const [selectedSupplier, setSelectedSupplier] = useState<any>(null);

  const filteredSuppliers = suppliers.filter(
    (supplier) =>
      supplier.name.toLowerCase().includes(search.toLowerCase()) ||
      supplier.contact.toLowerCase().includes(search.toLowerCase())
  );

  const handleAddSupplier = (supplier: any) => {
    setSuppliers((prev) => [...prev, supplier]);
    toast({
      title: "Fornecedor adicionado",
      description: `O fornecedor "${supplier.name}" foi criado com sucesso.`,
    });
  };

  const handleEditSupplier = (updatedSupplier: any) => {
    setSuppliers((prev) =>
      prev.map((s) => (s.id === updatedSupplier.id ? updatedSupplier : s))
    );
    toast({
      title: "Fornecedor atualizado",
      description: `As informações de "${updatedSupplier.name}" foram atualizadas.`,
    });
  };

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">Fornecedores</h2>
          <p className="text-muted-foreground">
            Gerencie fornecedores e parceiros
          </p>
        </div>
        <Button onClick={() => setIsDialogOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Adicionar Fornecedor
        </Button>
      </div>

      {/* Modais */}
      <NewSupplierDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        onCreate={handleAddSupplier}
      />

      <EditSupplierDialog
        open={isEditDialogOpen}
        onOpenChange={setIsEditDialogOpen}
        supplier={selectedSupplier}
        onSave={handleEditSupplier}
      />

      <Card>
        <CardHeader>
          <CardTitle>Lista de Fornecedores</CardTitle>
          <CardDescription>
            Visualize e gerencie todos os fornecedores
          </CardDescription>
        </CardHeader>
        <CardContent>
          <div className="mb-4">
            <div className="relative">
              <Search className="absolute left-2 top-2.5 h-4 w-4 text-muted-foreground" />
              <Input
                placeholder="Buscar fornecedores..."
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                className="pl-8"
              />
            </div>
          </div>

          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Nome</TableHead>
                <TableHead>Contato</TableHead>
                <TableHead>Email</TableHead>
                <TableHead>Telefone</TableHead>
                <TableHead>Localização</TableHead>
                <TableHead className="text-right">Ações</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {filteredSuppliers.map((supplier) => (
                <TableRow key={supplier.id}>
                  <TableCell className="font-medium">{supplier.name}</TableCell>
                  <TableCell>{supplier.contact}</TableCell>
                  <TableCell>{supplier.email}</TableCell>
                  <TableCell>{supplier.phone}</TableCell>
                  <TableCell>{supplier.address}</TableCell>
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
                        <DropdownMenuItem
                          onClick={() => {
                            setSelectedSupplier(supplier);
                            setIsEditDialogOpen(true);
                          }}
                        >
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
