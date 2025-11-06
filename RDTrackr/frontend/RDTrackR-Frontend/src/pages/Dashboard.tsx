import { DollarSign, ShoppingCart, Users, TrendingUp } from "lucide-react";
import { KpiCard } from "@/components/dashboard/KpiCard";
import { RevenueChart } from "@/components/dashboard/RevenueChart";
import { OrdersChart } from "@/components/dashboard/OrdersChart";
import { RecentOrdersTable } from "@/components/dashboard/RecentOrdersTable";

export default function Dashboard() {
  return (
    <div className="space-y-6">
      <div>
        <h2 className="text-3xl font-bold tracking-tight">Dashboard</h2>
        <p className="text-muted-foreground">
          Visão geral do desempenho do seu negócio
        </p>
      </div>

      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
        <KpiCard
          title="Receita Total"
          value="R$ 45.231,00"
          change={12.5}
          icon={DollarSign}
        />
        <KpiCard
          title="Pedidos"
          value="1.234"
          change={8.2}
          icon={ShoppingCart}
        />
        <KpiCard
          title="Usuários"
          value="573"
          change={-2.4}
          icon={Users}
        />
        <KpiCard
          title="Taxa de Conversão"
          value="3.24%"
          change={4.1}
          icon={TrendingUp}
        />
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <RevenueChart />
        <OrdersChart />
      </div>

      <RecentOrdersTable />
    </div>
  );
}
