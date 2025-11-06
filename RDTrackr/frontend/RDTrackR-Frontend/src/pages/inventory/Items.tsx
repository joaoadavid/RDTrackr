import { useState } from "react";
import {
  Search,
  Plus,
  MoreHorizontal,
  Pencil,
  Trash2,
  Package,
} from "lucide-react";
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
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { useToast } from "@/hooks/use-toast";
import { NewItemDialog } from "@/components/inventory/NewItemDialog";
import { EditItemDialog } from "@/components/inventory/EditItemDialog";

// Mock data - will be replaced with API data
const mockProducts = [
  {
    id: 1,
    sku: "NB-001",
    name: "Notebook Dell",
    category: "Eletrônicos",
    uom: "UN",
    price: 3499.0,
    stock: 45,
    reorderPoint: 15,
    updatedAt: "2025-10-29",
  },
  {
    id: 2,
    sku: "MS-002",
    name: "Mouse Logitech",
    category: "Eletrônicos",
    uom: "UN",
    price: 129.0,
    stock: 8,
    reorderPoint: 20,
    updatedAt: "2025-10-28",
  },
  {
    id: 3,
    sku: "KB-003",
    name: "Teclado Mecânico",
    category: "Eletrônicos",
    uom: "UN",
    price: 459.0,
    stock: 0,
    reorderPoint: 10,
    updatedAt: "2025-10-27",
  },
];

export default function InventoryItems() {
  const { toast } = useToast();
  const [products, setProducts] = useState(mockProducts);
  const [search, setSearch] = useState("");
  const [category, setCategory] = useState<string>("all");
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false);
  const [selectedItem, setSelectedItem] = useState<any>(null);

  // Adicionar item
  const handleAddItem = (item: any) => {
    setProducts((prev) => [...prev, item]);
  };

  // Editar item
  const handleUpdateItem = (updated: any) => {
    setProducts((prev) => prev.map((p) => (p.id === updated.id ? updated : p)));
    toast({
      title: "Item atualizado",
      description: `O produto "${updated.name}" foi atualizado com sucesso.`,
    });
  };

  // Excluir item
  const handleDelete = (id: number) => {
    const product = products.find((p) => p.id === id);
    if (!product) return;

    setProducts((prev) => prev.filter((p) => p.id !== id));
    toast({
      title: "Item excluído",
      description: `O produto "${product.name}" foi removido do inventário.`,
      variant: "destructive",
    });
  };

  const filteredProducts = products.filter(
    (product) =>
      (product.name.toLowerCase().includes(search.toLowerCase()) ||
        product.sku.toLowerCase().includes(search.toLowerCase())) &&
      (category === "all" || product.category === category)
  );

  const getStockStatus = (stock: number, reorderPoint: number) => {
    if (stock === 0)
      return { label: "Sem Estoque", variant: "destructive" as const };
    if (stock < reorderPoint)
      return { label: "Estoque Baixo", variant: "secondary" as const };
    return { label: "Em Estoque", variant: "default" as const };
  };

  return (
    <div className="space-y-6">
      {/* Cabeçalho */}
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">
            Itens do Estoque
          </h2>
          <p className="text-muted-foreground">
            Gerencie produtos e níveis de estoque
          </p>
        </div>
        <Button onClick={() => setIsDialogOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Adicionar Item
        </Button>
      </div>

      {/* Modais */}
      <NewItemDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        onCreate={handleAddItem}
      />
      <EditItemDialog
        open={isEditDialogOpen}
        onOpenChange={setIsEditDialogOpen}
        item={selectedItem}
        onUpdate={handleUpdateItem}
      />

      {/* Tabela */}
      <Card>
        <CardHeader>
          <CardTitle>Catálogo de Produtos</CardTitle>
          <CardDescription>
            Visualize e gerencie todos os produtos do inventário
          </CardDescription>
        </CardHeader>
        <CardContent>
          {/* Filtros */}
          <div className="mb-4 flex flex-col gap-4 sm:flex-row">
            <div className="relative flex-1">
              <Search className="absolute left-2 top-2.5 h-4 w-4 text-muted-foreground" />
              <Input
                placeholder="Buscar por nome ou SKU..."
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                className="pl-8"
              />
            </div>
            <Select value={category} onValueChange={setCategory}>
              <SelectTrigger className="w-full sm:w-[200px]">
                <SelectValue placeholder="Categoria" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="all">Todas</SelectItem>
                <SelectItem value="Eletrônicos">Eletrônicos</SelectItem>
                <SelectItem value="Roupas">Roupas</SelectItem>
                <SelectItem value="Alimentos">Alimentos</SelectItem>
              </SelectContent>
            </Select>
          </div>

          {/* Tabela */}
          <div className="rounded-md border">
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>SKU</TableHead>
                  <TableHead>Produto</TableHead>
                  <TableHead>Categoria</TableHead>
                  <TableHead>UoM</TableHead>
                  <TableHead className="text-right">Preço</TableHead>
                  <TableHead className="text-right">Estoque</TableHead>
                  <TableHead className="text-right">Reorder</TableHead>
                  <TableHead>Status</TableHead>
                  <TableHead className="text-right">Ações</TableHead>
                </TableRow>
              </TableHeader>

              <TableBody>
                {filteredProducts.map((product) => {
                  const status = getStockStatus(
                    product.stock,
                    product.reorderPoint
                  );
                  return (
                    <TableRow key={product.id}>
                      <TableCell className="font-medium">
                        {product.sku}
                      </TableCell>
                      <TableCell>{product.name}</TableCell>
                      <TableCell>{product.category}</TableCell>
                      <TableCell>{product.uom}</TableCell>
                      <TableCell className="text-right">
                        R$ {product.price.toFixed(2)}
                      </TableCell>
                      <TableCell className="text-right">
                        {product.stock}
                      </TableCell>
                      <TableCell className="text-right">
                        {product.reorderPoint}
                      </TableCell>
                      <TableCell>
                        <Badge variant={status.variant}>{status.label}</Badge>
                      </TableCell>
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
                                setSelectedItem(product);
                                setIsEditDialogOpen(true);
                              }}
                            >
                              <Pencil className="mr-2 h-4 w-4" />
                              Editar
                            </DropdownMenuItem>
                            <DropdownMenuItem
                              onClick={() => handleDelete(product.id)}
                              className="text-destructive"
                            >
                              <Trash2 className="mr-2 h-4 w-4" />
                              Excluir
                            </DropdownMenuItem>
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </TableCell>
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </div>
        </CardContent>
      </Card>
    </div>
  );
}
