/// <reference types="@testing-library/jest-dom" />

import { render, screen, waitFor, within } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import Movements from "@/pages/inventory/Movements";

describe("Movements page", () => {
  it("deve renderizar as movimentações mockadas", () => {
    render(<Movements />);

    expect(screen.getByText("Notebook Dell")).toBeInTheDocument();
    expect(screen.getByText("Mouse Logitech")).toBeInTheDocument();
    expect(screen.getByText("Teclado Mecânico")).toBeInTheDocument();
  });

  it("deve criar uma nova movimentação", async () => {
    const user = userEvent.setup();
    render(<Movements />);

    await user.click(
      screen.getByRole("button", { name: /nova movimentação/i })
    );

    await user.type(screen.getByLabelText(/referência/i), "PO-999");
    await user.type(screen.getByLabelText(/produto/i), "Monitor Samsung");
    await user.type(screen.getByLabelText(/depósito/i), "CD São Paulo");
    await user.type(screen.getByLabelText(/quantidade/i), "12");

    await user.click(
      screen.getByRole("button", { name: /salvar movimentação/i })
    );

    await waitFor(() => {
      const row = screen.getByText("PO-999").closest("tr")!;
      expect(within(row).getByText("Monitor Samsung")).toBeInTheDocument();
      expect(within(row).getByText("CD São Paulo")).toBeInTheDocument();
      expect(within(row).getByText("+12")).toBeInTheDocument();
    });
  });
});
