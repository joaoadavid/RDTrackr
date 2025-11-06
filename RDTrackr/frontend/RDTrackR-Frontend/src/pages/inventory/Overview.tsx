import { Link } from "react-router-dom";
import {
  Package,
  Warehouse,
  TrendingDown,
  Activity,
  ShoppingBag,
} from "lucide-react";
import { KpiCard } from "@/components/dashboard/KpiCard";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import {
  Area,
  AreaChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
  Legend,
} from "recharts";

// Mock data - will be replaced with real API data
const movementsData = [
  { month: "Jan", inbound: 450, outbound: 320 },
  { month: "Fev", inbound: 380, outbound: 290 },
  { month: "Mar", inbound: 520, outbound: 410 },
  { month: "Abr", inbound: 490, outbound: 380 },
  { month: "Mai", inbound: 610, outbound: 520 },
  { month: "Jun", inbound: 580, outbound: 490 },
];

const lowStockItems = [
  {
    id: 1,
    sku: "PRD-001",
    name: "Notebook Dell",
    current: 8,
    reorder: 15,
    status: "critical",
  },
  {
    id: 2,
    sku: "PRD-002",
    name: "Mouse Logitech",
    current: 12,
    reorder: 20,
    status: "warning",
  },
  {
    id: 3,
    sku: "PRD-003",
    name: "Teclado Mecânico",
    current: 0,
    reorder: 10,
    status: "critical",
  },
  {
    id: 4,
    sku: "PRD-004",
    name: 'Monitor LG 27"',
    current: 18,
    reorder: 25,
    status: "warning",
  },
];

export default function InventoryOverview() {
  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-3xl font-bold tracking-tight">
          Estoque - Visão Geral
        </h2>
        <p className="text-muted-foreground">
          Acompanhe métricas e alertas do inventário
        </p>
      </div>

      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
        <KpiCard
          title="Produtos Ativos"
          value="247"
          icon={Package}
          description="Total de SKUs cadastrados"
        />
        <KpiCard
          title="Estoque Total"
          value="12.458"
          icon={Warehouse}
          description="Unidades em todos os depósitos"
        />
        <KpiCard
          title="Abaixo do Reorder"
          value="4"
          icon={TrendingDown}
          description="Itens com estoque baixo"
        />
        <KpiCard
          title="Movimentações (7d)"
          value="183"
          icon={Activity}
          description="Entradas e saídas"
        />
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Entradas vs Saídas</CardTitle>
          <CardDescription>Movimentações mensais do estoque</CardDescription>
        </CardHeader>
        <CardContent>
          <ResponsiveContainer width="100%" height={300}>
            <AreaChart data={movementsData}>
              <defs>
                <linearGradient id="colorInbound" x1="0" y1="0" x2="0" y2="1">
                  <stop
                    offset="5%"
                    stopColor="hsl(var(--chart-2))"
                    stopOpacity={0.3}
                  />
                  <stop
                    offset="95%"
                    stopColor="hsl(var(--chart-2))"
                    stopOpacity={0}
                  />
                </linearGradient>
                <linearGradient id="colorOutbound" x1="0" y1="0" x2="0" y2="1">
                  <stop
                    offset="5%"
                    stopColor="hsl(var(--chart-3))"
                    stopOpacity={0.3}
                  />
                  <stop
                    offset="95%"
                    stopColor="hsl(var(--chart-3))"
                    stopOpacity={0}
                  />
                </linearGradient>
              </defs>
              <XAxis
                dataKey="month"
                stroke="hsl(var(--muted-foreground))"
                fontSize={12}
              />
              <YAxis stroke="hsl(var(--muted-foreground))" fontSize={12} />
              <Tooltip
                contentStyle={{
                  backgroundColor: "hsl(var(--popover))",
                  border: "1px solid hsl(var(--border))",
                  borderRadius: "var(--radius)",
                }}
              />
              <Legend />
              <Area
                type="monotone"
                dataKey="inbound"
                name="Entradas"
                stroke="hsl(var(--chart-2))"
                fillOpacity={1}
                fill="url(#colorInbound)"
                strokeWidth={2}
              />
              <Area
                type="monotone"
                dataKey="outbound"
                name="Saídas"
                stroke="hsl(var(--chart-3))"
                fillOpacity={1}
                fill="url(#colorOutbound)"
                strokeWidth={2}
              />
            </AreaChart>
          </ResponsiveContainer>
        </CardContent>
      </Card>

      <Card>
        <CardHeader className="flex flex-row items-center justify-between">
          <div>
            <CardTitle>Itens com Estoque Baixo</CardTitle>
            <CardDescription>
              Produtos abaixo do ponto de reposição
            </CardDescription>
          </div>
          <Button asChild>
            <Link to="/inventory/purchase-orders">
              <ShoppingBag className="mr-2 h-4 w-4" />
              Criar Pedido de Compra
            </Link>
          </Button>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>SKU</TableHead>
                <TableHead>Produto</TableHead>
                <TableHead>Atual</TableHead>
                <TableHead>Reorder Point</TableHead>
                <TableHead>Status</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {lowStockItems.map((item) => (
                <TableRow key={item.id}>
                  <TableCell className="font-medium">{item.sku}</TableCell>
                  <TableCell>{item.name}</TableCell>
                  <TableCell>{item.current}</TableCell>
                  <TableCell>{item.reorder}</TableCell>
                  <TableCell>
                    <Badge
                      variant={
                        item.status === "critical" ? "destructive" : "secondary"
                      }
                    >
                      {item.status === "critical" ? "Crítico" : "Baixo"}
                    </Badge>
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
