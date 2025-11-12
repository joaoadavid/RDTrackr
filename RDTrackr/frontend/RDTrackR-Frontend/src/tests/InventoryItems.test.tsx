import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import InventoryItems from "@/pages/inventory/Items";

describe("InventoryItems page", () => {
  it("deve renderizar os produtos mockados", () => {
    render(<InventoryItems />);

    expect(screen.getByText("Notebook Dell")).toBeInTheDocument();
    expect(screen.getByText("Mouse Logitech")).toBeInTheDocument();
    expect(screen.getByText("Teclado Mecânico")).toBeInTheDocument();
  });

  it("deve filtrar produtos usando a busca", async () => {
    const user = userEvent.setup();
    render(<InventoryItems />);

    const searchInput = screen.getByPlaceholderText(/buscar/i);
    await user.type(searchInput, "Mouse");

    expect(screen.queryByText("Notebook Dell")).not.toBeInTheDocument();
    expect(screen.getByText("Mouse Logitech")).toBeInTheDocument();
  });

  it("deve excluir um item", async () => {
    const user = userEvent.setup();
    render(<InventoryItems />);

    const row = screen.getByText("Mouse Logitech").closest("tr")!;

    const menuButton = row.querySelector("button")!;
    await user.click(menuButton);

    const deleteOption = screen.getByText("Excluir");
    await user.click(deleteOption);

    await waitFor(() => {
      expect(screen.queryByText("Mouse Logitech")).not.toBeInTheDocument();
    });
  });
});
