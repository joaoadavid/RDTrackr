import * as matchers from "@testing-library/jest-dom/matchers";
import { expect, vi } from "vitest";

// Corrige erros do Radix no JSDOM
HTMLElement.prototype.hasPointerCapture = () => false;
HTMLElement.prototype.setPointerCapture = () => {};
HTMLElement.prototype.releasePointerCapture = () => {};
HTMLElement.prototype.scrollIntoView = vi.fn();

// ✅ Corrige erro do Recharts (ResponsiveContainer)
class ResizeObserverMock {
  observe() {}
  unobserve() {}
  disconnect() {}
}

// Aplica no escopo global compatível com vitest
globalThis.ResizeObserver = ResizeObserverMock as any;
window.ResizeObserver = ResizeObserverMock as any;

expect.extend(matchers);
