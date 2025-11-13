import { ApiClient } from "@/generated/apiClient";
import { customFetch } from "./customFetch";

export const api = new ApiClient(import.meta.env.VITE_API_URL, {
  fetch: customFetch,
});
