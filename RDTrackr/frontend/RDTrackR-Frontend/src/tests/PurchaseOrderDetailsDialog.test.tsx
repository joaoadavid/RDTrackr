import { render, screen, within } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { vi } from "vitest";
import { PurchaseOrderDetailsDialog } from "@/components/purchase-orders/PurchaseOrderDetailsDialog";

describe("PurchaseOrderDetailsDialog", () => {
  const onOpenChangeMock = vi.fn();

  const mockOrder = {
    id: 1,
    number: "PO-250101-001",
    supplier: "TechSupply Brasil",
    status: "APPROVED",
    createdAt: "2025-01-01",
  };

  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("deve renderizar corretamente os detalhes do pedido", () => {
    render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={onOpenChangeMock}
        order={mockOrder}
      />
    );

    expect(screen.getByTestId("details-title")).toHaveTextContent(
      "Detalhes do Pedido"
    );
    // verifica que existem duas instâncias de "Subtotal" (tabela + totais)
    expect(screen.getAllByText(/Subtotal/)).toHaveLength(2);

    // garante que textos de totais estão presentes
    expect(screen.getAllByText(/Impostos/).length).toBeGreaterThan(0);
    expect(screen.getAllByText(/Total/).length).toBeGreaterThan(0);
  });

  it("deve renderizar corretamente o status CANCELADO", () => {
    const orderCancelled = { ...mockOrder, status: "CANCELLED" };
    render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={onOpenChangeMock}
        order={orderCancelled}
      />
    );
    expect(screen.getByText("Cancelado")).toBeInTheDocument();
  });

  it("deve renderizar corretamente o status PENDENTE", () => {
    const orderPending = { ...mockOrder, status: "PENDING" };
    render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={onOpenChangeMock}
        order={orderPending}
      />
    );
    expect(screen.getByText("Pendente")).toBeInTheDocument();
  });

  it("deve renderizar corretamente o status RASCUNHO", () => {
    const orderDraft = { ...mockOrder, status: "DRAFT" };
    render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={onOpenChangeMock}
        order={orderDraft}
      />
    );
    expect(screen.getByText("Rascunho")).toBeInTheDocument();
  });

  it("deve exibir os itens da tabela corretamente", () => {
    render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={vi.fn()}
        order={{
          id: 1,
          number: "PO-250101-001",
          supplier: "TechSupply Brasil",
          status: "APPROVED",
          createdAt: "2025-01-01",
        }}
      />
    );

    // produtos
    expect(screen.getByText("Notebook Dell Inspiron 15")).toBeInTheDocument();
    expect(screen.getByText("Mouse Logitech M170")).toBeInTheDocument();
    expect(screen.getByText(/Monitor LG Ultrawide 29/)).toBeInTheDocument();

    // pega todos os textos "Subtotal", "Impostos" e "Total" e garante que existem
    expect(
      screen.getAllByText((_, node) => node?.textContent?.includes("Subtotal"))
        .length
    ).toBeGreaterThan(0);
    expect(
      screen.getAllByText((_, node) => node?.textContent?.includes("Impostos"))
        .length
    ).toBeGreaterThan(0);
    expect(
      screen.getAllByText((_, node) => node?.textContent?.includes("Total"))
        .length
    ).toBeGreaterThan(0);
  });

  it("deve fechar o dialog quando o botão 'Fechar' for clicado", async () => {
    const user = userEvent.setup();

    render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={onOpenChangeMock}
        order={mockOrder}
      />
    );

    await user.click(screen.getByRole("button", { name: /fechar/i }));

    expect(onOpenChangeMock).toHaveBeenCalledWith(false);
  });

  it("não deve renderizar nada se order for null", () => {
    const { container } = render(
      <PurchaseOrderDetailsDialog
        open
        onOpenChange={onOpenChangeMock}
        order={null}
      />
    );

    expect(container.firstChild).toBeNull();
  });
});
