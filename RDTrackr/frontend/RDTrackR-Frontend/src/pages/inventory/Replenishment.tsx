import { useState, useMemo } from "react";
import {
  AlertTriangle,
  Package,
  TrendingDown,
  DollarSign,
  RefreshCw,
} from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { KpiCard } from "@/components/inventory/KpiCard";
import { ControlsBar } from "@/components/inventory/ControlsBar";
import {
  ReplenishmentTable,
  ReplenishmentItem,
} from "@/components/inventory/ReplenishmentTable";
import { GeneratePoDialog } from "@/components/inventory/GeneratePoDialog";
import { useToast } from "@/hooks/use-toast";
import {
  mockProducts,
  mockStockSummary,
  mockConsumption,
} from "@/lib/mock-data";

export default function Replenishment() {
  const { toast } = useToast();

  // Parâmetros
  const [searchTerm, setSearchTerm] = useState("");
  const [category, setCategory] = useState("all");
  const [window, setWindow] = useState(60);
  const [seasonality, setSeasonality] = useState(0);
  const [coverageDays, setCoverageDays] = useState(0);

  // Seleção e edição
  const [selectedIds, setSelectedIds] = useState<Set<string>>(new Set());
  const [editedQty, setEditedQty] = useState<Record<string, number>>({});
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  // Cálculo dos itens
  const replenishmentItems = useMemo(() => {
    return mockProducts.map((product) => {
      const stock = mockStockSummary.find((s) => s.productId === product.id);
      const consumption = mockConsumption.find(
        (c) => c.productId === product.id
      );

      const currentStock = stock?.currentStock || 0;
      const totalConsumed = consumption?.totalConsumed || 0;
      const periodRatio = window / (consumption?.period || 60);
      const adjustedConsumption = totalConsumed * periodRatio;
      const dailyConsumption =
        (adjustedConsumption / window) * (1 + seasonality / 100);

      const demandLeadTime = dailyConsumption * product.leadTime;
      const effectiveROP = product.safetyStock + demandLeadTime;
      const coverageExtra = dailyConsumption * coverageDays;

      const suggestedQty = Math.max(
        0,
        effectiveROP + coverageExtra - currentStock
      );

      const isCritical = currentStock <= effectiveROP;

      return {
        id: product.id,
        sku: product.sku,
        name: product.name,
        category: product.category,
        uom: product.uom,
        currentStock,
        reorderPoint: effectiveROP,
        dailyConsumption,
        leadTime: product.leadTime,
        suggestedQty: editedQty[product.id] ?? suggestedQty,
        isCritical,
        unitPrice: product.unitPrice,
      } as ReplenishmentItem;
    });
  }, [window, seasonality, coverageDays, editedQty]);

  // Filtros
  const filteredItems = useMemo(() => {
    return replenishmentItems.filter((item) => {
      const matchesSearch =
        item.sku.toLowerCase().includes(searchTerm.toLowerCase()) ||
        item.name.toLowerCase().includes(searchTerm.toLowerCase());
      const matchesCategory = category === "all" || item.category === category;
      return matchesSearch && matchesCategory;
    });
  }, [replenishmentItems, searchTerm, category]);

  // KPIs
  const kpis = useMemo(() => {
    const criticalCount = filteredItems.filter((i) => i.isCritical).length;
    const totalStock = filteredItems.reduce(
      (sum, i) => sum + i.currentStock,
      0
    );
    const totalDailyConsumption = filteredItems.reduce(
      (sum, i) => sum + i.dailyConsumption,
      0
    );
    const estimatedValue = filteredItems.reduce(
      (sum, i) => sum + i.suggestedQty * i.unitPrice,
      0
    );

    return {
      criticalCount,
      totalStock,
      totalDailyConsumption,
      estimatedValue,
    };
  }, [filteredItems]);

  // Handlers
  const handleRecalculate = () => {
    toast({
      title: "Recalculado",
      description: "Os valores foram atualizados com os novos parâmetros.",
    });
  };

  const handleToggleItem = (id: string) => {
    setSelectedIds((prev) => {
      const next = new Set(prev);
      next.has(id) ? next.delete(id) : next.add(id);
      return next;
    });
  };

  const handleToggleAll = () => {
    if (selectedIds.size === filteredItems.length) {
      setSelectedIds(new Set());
    } else {
      setSelectedIds(new Set(filteredItems.map((i) => i.id)));
    }
  };

  const handleSelectCritical = () => {
    const criticalIds = filteredItems
      .filter((i) => i.isCritical)
      .map((i) => i.id);
    setSelectedIds(new Set(criticalIds));
  };

  const handleQtyChange = (id: string, qty: number) => {
    setEditedQty((prev) => ({ ...prev, [id]: qty }));
  };

  const handleGeneratePo = () => {
    if (selectedIds.size === 0) {
      toast({
        title: "Nenhum item selecionado",
        description: "Selecione ao menos um item para gerar o pedido.",
        variant: "destructive",
      });
      return;
    }
    setIsDialogOpen(true);
  };

  const handleConfirmPo = (
    supplierId: string,
    notes: string,
    groupBySupplier: boolean
  ) => {
    const selectedItems = filteredItems.filter((i) => selectedIds.has(i.id));

    console.log("Gerando PO:", {
      supplierId,
      notes,
      groupBySupplier,
      items: selectedItems.map((i) => ({
        productId: i.id,
        qty: i.suggestedQty,
        unitPrice: i.unitPrice,
      })),
    });

    toast({
      title: "Pedido criado",
      description: `Pedido de compra criado com ${selectedItems.length} item(ns).`,
    });

    setIsDialogOpen(false);
    setSelectedIds(new Set());
  };

  return (
    <div className="space-y-6">
      {/* Título e botão de ação */}
      <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">
            Planejamento de Reposição
          </h2>
          <p className="text-muted-foreground">
            Calcule sugestões de compra baseadas em consumo médio, lead time e
            estoque de segurança.
          </p>
        </div>
        <Button variant="outline" onClick={handleRecalculate}>
          <RefreshCw className="mr-2 h-4 w-4" />
          Recalcular
        </Button>
      </div>

      {/* KPIs */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        <KpiCard
          title="Itens críticos"
          value={kpis.criticalCount}
          icon={AlertTriangle}
        />
        <KpiCard
          title="Estoque total"
          value={kpis.totalStock.toFixed(0)}
          icon={Package}
        />
        <KpiCard
          title="Consumo diário"
          value={kpis.totalDailyConsumption.toFixed(1)}
          icon={TrendingDown}
        />
        <KpiCard
          title="Valor estimado"
          value={`R$ ${kpis.estimatedValue.toFixed(2)}`}
          icon={DollarSign}
        />
      </div>

      {/* Filtros */}
      <Card>
        <CardHeader>
          <CardTitle>Parâmetros de Simulação</CardTitle>
          <CardDescription>
            Ajuste os filtros e parâmetros para recalcular as sugestões
          </CardDescription>
        </CardHeader>
        <CardContent>
          <ControlsBar
            searchTerm={searchTerm}
            onSearchChange={setSearchTerm}
            category={category}
            onCategoryChange={setCategory}
            window={window}
            onWindowChange={setWindow}
            seasonality={seasonality}
            onSeasonalityChange={setSeasonality}
            coverageDays={coverageDays}
            onCoverageDaysChange={setCoverageDays}
            onRecalculate={handleRecalculate}
          />
        </CardContent>
      </Card>

      {/* Tabela */}
      <Card>
        <CardHeader>
          <CardTitle>Itens de Reposição</CardTitle>
          <CardDescription>
            Produtos com cálculo automático de ponto de pedido
          </CardDescription>
        </CardHeader>
        <CardContent>
          <ReplenishmentTable
            items={filteredItems}
            selectedIds={selectedIds}
            onToggleItem={handleToggleItem}
            onToggleAll={handleToggleAll}
            onSelectCritical={handleSelectCritical}
            onQtyChange={handleQtyChange}
          />
        </CardContent>
      </Card>

      {/* Botão Gerar PO */}
      {selectedIds.size > 0 && (
        <div className="flex justify-end">
          <Button onClick={handleGeneratePo} size="lg">
            Gerar Pedido ({selectedIds.size} item
            {selectedIds.size !== 1 ? "s" : ""})
          </Button>
        </div>
      )}

      {/* Dialog */}
      <GeneratePoDialog
        open={isDialogOpen}
        onOpenChange={setIsDialogOpen}
        items={filteredItems.filter((i) => selectedIds.has(i.id))}
        onConfirm={handleConfirmPo}
      />
    </div>
  );
}
