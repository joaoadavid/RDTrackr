import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { within } from "@testing-library/react";
import PurchaseOrders from "@/pages/inventory/PurchaseOrders";

describe("PurchaseOrders Page", () => {
  it("deve exibir o pedido PO-2025-003 com valor formatado corretamente", () => {
    render(<PurchaseOrders />);

    expect(
      screen.getByRole("heading", { level: 2, name: /pedidos de compra/i })
    ).toBeInTheDocument();

    expect(screen.getByText("PO-2025-003")).toBeInTheDocument();
    expect(screen.getByText("GlobalTech Solutions")).toBeInTheDocument();
    expect(screen.getByText(/recebido/i)).toBeInTheDocument();

    expect(
      screen.getByText((content) => content.includes("45.780,00"))
    ).toBeInTheDocument();
  });

  it("deve filtrar pedidos com status 'Aprovado'", async () => {
    const user = userEvent.setup();
    render(<PurchaseOrders />);

    // Abre o Select
    await user.click(screen.getByTestId("status-filter-trigger"));

    // Aguarda menu aparecer
    const menu = await screen.findByTestId("status-filter-menu");

    // Seleciona "Aprovado"
    await user.click(within(menu).getByText(/Aprovado/i));

    expect(screen.getByText("PO-2025-001")).toBeInTheDocument(); // APPROVED

    expect(screen.queryByText("PO-2025-002")).not.toBeInTheDocument(); // PENDING
    expect(screen.queryByText("PO-2025-003")).not.toBeInTheDocument(); // RECEIVED
    expect(screen.queryByText("PO-2025-004")).not.toBeInTheDocument(); // DRAFT
    expect(screen.queryByText("PO-2025-005")).not.toBeInTheDocument(); // CANCELLED
  });
});
