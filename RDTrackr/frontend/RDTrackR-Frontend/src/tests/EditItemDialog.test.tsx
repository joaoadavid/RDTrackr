import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { EditItemDialog } from "@/components/inventory/EditItemDialog";

describe("EditItemDialog", () => {
  const mockItem = {
    sku: "PROD-001",
    name: "Mouse Gamer",
    category: "Periféricos",
    uom: "UN",
    price: "150.00",
    stock: "10",
    reorderPoint: "3",
  };

  it("deve carregar os valores iniciais do item", () => {
    render(
      <EditItemDialog
        open={true}
        onOpenChange={() => {}}
        item={mockItem}
        onUpdate={() => {}}
      />
    );

    expect(screen.getByDisplayValue("PROD-001")).toBeInTheDocument();
    expect(screen.getByDisplayValue("Mouse Gamer")).toBeInTheDocument();
    expect(screen.getByDisplayValue("Periféricos")).toBeInTheDocument();

    // ✅ Troca aqui ↓
    expect(screen.getByLabelText("Unidade")).toBeInTheDocument();

    expect(screen.getByDisplayValue("150.00")).toBeInTheDocument();
    expect(screen.getByDisplayValue("10")).toBeInTheDocument();
    expect(screen.getByDisplayValue("3")).toBeInTheDocument();
  });

  it("deve permitir edição e enviar os dados atualizados", async () => {
    const user = userEvent.setup();
    const onUpdateMock = vi.fn();

    render(
      <EditItemDialog
        open={true}
        onOpenChange={() => {}}
        item={mockItem}
        onUpdate={onUpdateMock}
      />
    );

    const nameInput = screen.getByLabelText("Nome");
    await user.clear(nameInput);
    await user.type(nameInput, "Mouse Gamer Pro");

    await user.click(
      screen.getByRole("button", { name: /salvar alterações/i })
    );

    expect(onUpdateMock).toHaveBeenCalledTimes(1);
    expect(onUpdateMock).toHaveBeenCalledWith(
      expect.objectContaining({ name: "Mouse Gamer Pro" })
    );
  });
});
