// Mock data para módulo de reposição/estoque

export interface Product {
  id: string;
  sku: string;
  name: string;
  category: string;
  uom: string;
  leadTime: number; // dias
  safetyStock: number;
  reorderPoint: number;
  unitPrice: number;
}

export interface StockSummary {
  productId: string;
  currentStock: number;
}

export interface Consumption {
  productId: string;
  totalConsumed: number;
  period: number; // dias
}

export interface Supplier {
  id: string;
  name: string;
  email: string;
}

export const mockProducts: Product[] = [
  {
    id: "p1",
    sku: "MT-001",
    name: "Aço ABNT 1020 Ø 25mm",
    category: "Matéria-prima",
    uom: "kg",
    leadTime: 15,
    safetyStock: 200,
    reorderPoint: 350,
    unitPrice: 8.5,
  },
  {
    id: "p2",
    sku: "MT-002",
    name: "Alumínio 6061 Ø 50mm",
    category: "Matéria-prima",
    uom: "kg",
    leadTime: 20,
    safetyStock: 150,
    reorderPoint: 280,
    unitPrice: 22.0,
  },
  {
    id: "p3",
    sku: "FT-101",
    name: "Broca HSS Ø 10mm",
    category: "Ferramenta",
    uom: "un",
    leadTime: 10,
    safetyStock: 20,
    reorderPoint: 35,
    unitPrice: 45.0,
  },
  {
    id: "p4",
    sku: "FT-102",
    name: "Fresa de Topo Ø 12mm",
    category: "Ferramenta",
    uom: "un",
    leadTime: 12,
    safetyStock: 15,
    reorderPoint: 25,
    unitPrice: 120.0,
  },
  {
    id: "p5",
    sku: "CM-201",
    name: "Óleo de Corte Sintético",
    category: "Consumível",
    uom: "L",
    leadTime: 7,
    safetyStock: 100,
    reorderPoint: 180,
    unitPrice: 12.5,
  },
  {
    id: "p6",
    sku: "CM-202",
    name: "Estopa Industrial",
    category: "Consumível",
    uom: "kg",
    leadTime: 5,
    safetyStock: 30,
    reorderPoint: 50,
    unitPrice: 6.0,
  },
  {
    id: "p7",
    sku: "MT-003",
    name: "Aço Inox 304 Ø 30mm",
    category: "Matéria-prima",
    uom: "kg",
    leadTime: 25,
    safetyStock: 100,
    reorderPoint: 220,
    unitPrice: 35.0,
  },
  {
    id: "p8",
    sku: "FT-103",
    name: "Inserto CNMG 120408",
    category: "Ferramenta",
    uom: "un",
    leadTime: 15,
    safetyStock: 50,
    reorderPoint: 80,
    unitPrice: 18.0,
  },
];

export const mockStockSummary: StockSummary[] = [
  { productId: "p1", currentStock: 280 },
  { productId: "p2", currentStock: 420 },
  { productId: "p3", currentStock: 32 },
  { productId: "p4", currentStock: 18 },
  { productId: "p5", currentStock: 250 },
  { productId: "p6", currentStock: 65 },
  { productId: "p7", currentStock: 150 },
  { productId: "p8", currentStock: 72 },
];

export const mockConsumption: Consumption[] = [
  { productId: "p1", totalConsumed: 840, period: 60 },
  { productId: "p2", totalConsumed: 480, period: 60 },
  { productId: "p3", totalConsumed: 90, period: 60 },
  { productId: "p4", totalConsumed: 72, period: 60 },
  { productId: "p5", totalConsumed: 600, period: 60 },
  { productId: "p6", totalConsumed: 180, period: 60 },
  { productId: "p7", totalConsumed: 360, period: 60 },
  { productId: "p8", totalConsumed: 120, period: 60 },
];

export const mockSuppliers: Supplier[] = [
  { id: "s1", name: "Metalúrgica São Paulo", email: "vendas@metalsp.com.br" },
  { id: "s2", name: "Ferramentaria Nacional", email: "contato@ferranacional.com.br" },
  { id: "s3", name: "Distribuidora de Consumíveis", email: "pedidos@distcons.com.br" },
];
