import { Toaster } from "@/components/ui/toaster";
import { Toaster as Sonner } from "@/components/ui/sonner";
import { TooltipProvider } from "@/components/ui/tooltip";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { SidebarProvider } from "@/components/ui/sidebar";
import { AppSidebar } from "@/components/layout/AppSidebar";
import { Topbar } from "@/components/layout/Topbar";
import { Breadcrumbs } from "@/components/layout/Breadcrumbs";
import { ThemeProvider } from "@/hooks/use-theme";
import Home from "./pages/Home";
import Contact from "./pages/Contact";
import Support from "./pages/Support";
import Register from "./pages/Register";
import ReplenishmentInfo from "./pages/Replenishment";
import Replenishment from "./pages/inventory/Replenishment";
import Dashboard from "./pages/Dashboard";
import Users from "./pages/Users";
import Products from "./pages/Products";
import Orders from "./pages/Orders";
import Reports from "./pages/Reports";
import Settings from "./pages/Settings";
import AuditLog from "./pages/AuditLog";
import Login from "./pages/Login";
import InventoryOverview from "./pages/inventory/Overview";
import InventoryItems from "./pages/inventory/Items";
import Warehouses from "./pages/inventory/Warehouses";
import Movements from "./pages/inventory/Movements";
import Suppliers from "./pages/inventory/Suppliers";
import PurchaseOrders from "./pages/inventory/PurchaseOrders";
import NotFound from "./pages/NotFound";

const queryClient = new QueryClient();

function AppLayout({ children }: { children: React.ReactNode }) {
  return (
    <SidebarProvider>
      <div className="flex min-h-screen w-full">
        <AppSidebar />
        <div className="flex-1 flex flex-col">
          <Topbar />
          <main className="flex-1 p-6">
            <div className="mb-4">
              <Breadcrumbs />
            </div>
            {children}
          </main>
        </div>
      </div>
    </SidebarProvider>
  );
}

const App = () => (
  <QueryClientProvider client={queryClient}>
    <ThemeProvider>
      <TooltipProvider>
        <Toaster />
        <Sonner />
        <BrowserRouter>
          <Routes>
            {/* ROTAS PÚBLICAS */}
            <Route path="/" element={<Home />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/support" element={<Support />} />
            <Route path="/replenishment-info" element={<ReplenishmentInfo />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login />} />
            <Route
              path="/dashboard"
              element={
                <AppLayout>
                  <Dashboard />
                </AppLayout>
              }
            />
            <Route
              path="/users"
              element={
                <AppLayout>
                  <Users />
                </AppLayout>
              }
            />
            <Route
              path="/products"
              element={
                <AppLayout>
                  <Products />
                </AppLayout>
              }
            />
            <Route
              path="/orders"
              element={
                <AppLayout>
                  <Orders />
                </AppLayout>
              }
            />
            <Route
              path="/reports"
              element={
                <AppLayout>
                  <Reports />
                </AppLayout>
              }
            />
            <Route
              path="/settings"
              element={
                <AppLayout>
                  <Settings />
                </AppLayout>
              }
            />
            <Route
              path="/audit-log"
              element={
                <AppLayout>
                  <AuditLog />
                </AppLayout>
              }
            />
            <Route
              path="/inventory"
              element={
                <AppLayout>
                  <InventoryOverview />
                </AppLayout>
              }
            />
            <Route
              path="/inventory/items"
              element={
                <AppLayout>
                  <InventoryItems />
                </AppLayout>
              }
            />
            <Route
              path="/inventory/warehouses"
              element={
                <AppLayout>
                  <Warehouses />
                </AppLayout>
              }
            />
            <Route
              path="/inventory/replenishment"
              element={
                <AppLayout>
                  <Replenishment />
                </AppLayout>
              }
            />
            <Route
              path="/inventory/movements"
              element={
                <AppLayout>
                  <Movements />
                </AppLayout>
              }
            />
            <Route
              path="/inventory/suppliers"
              element={
                <AppLayout>
                  <Suppliers />
                </AppLayout>
              }
            />
            <Route
              path="/inventory/purchase-orders"
              element={
                <AppLayout>
                  <PurchaseOrders />
                </AppLayout>
              }
            />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </BrowserRouter>
      </TooltipProvider>
    </ThemeProvider>
  </QueryClientProvider>
);

export default App;
