/// <reference types="@testing-library/jest-dom" />

import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import InventoryOverview from "@/pages/inventory/Overview";

describe("InventoryOverview Page", () => {
  function renderPage() {
    return render(
      <MemoryRouter>
        <InventoryOverview />
      </MemoryRouter>
    );
  }

  it("deve exibir o título principal e subtítulo", () => {
    renderPage();

    expect(
      screen.getByRole("heading", { name: /estoque - visão geral/i })
    ).toBeInTheDocument();

    expect(
      screen.getByText(/acompanhe métricas e alertas do inventário/i)
    ).toBeInTheDocument();
  });

  it("deve exibir os cartões de KPI", () => {
    renderPage();

    expect(screen.getByText("Produtos Ativos")).toBeInTheDocument();
    expect(screen.getByText("247")).toBeInTheDocument();
    expect(screen.getByText("Estoque Total")).toBeInTheDocument();
    expect(screen.getByText("12.458")).toBeInTheDocument();
    expect(screen.getByText("Abaixo do Reorder")).toBeInTheDocument();
    expect(screen.getByText("4")).toBeInTheDocument();
  });

  it("deve renderizar a tabela de itens com estoque baixo", () => {
    renderPage();

    expect(screen.getByText("Itens com Estoque Baixo")).toBeInTheDocument();

    // Verifica alguns itens mockados
    expect(screen.getByText("Notebook Dell")).toBeInTheDocument();
    expect(screen.getByText("Mouse Logitech")).toBeInTheDocument();
    expect(screen.getByText("Teclado Mecânico")).toBeInTheDocument();
  });

  it("deve ter o botão 'Criar Pedido de Compra' com link correto", () => {
    renderPage();

    const link = screen.getByRole("link", { name: /criar pedido de compra/i });

    expect(link).toBeInTheDocument();
    expect(link).toHaveAttribute("href", "/inventory/purchase-orders");
  });
});
