import { ApiError } from "./apiError";
import { api } from "./api";
import { RequestNewTokenJson } from "@/generated/apiClient";

export async function customFetch(input: RequestInfo, init?: RequestInit) {
  let accessToken = localStorage.getItem("accessToken");

  const newInit: RequestInit = {
    ...init,
    headers: {
      ...(init?.headers ?? {}),
      Authorization: accessToken ? `Bearer ${accessToken}` : "",
    },
  };

  let response = await fetch(input, newInit);

  // Token expirado -> tenta refresh
  if (response.status === 401) {
    const refreshToken = localStorage.getItem("refreshToken");

    if (refreshToken) {
      try {
        const body = new RequestNewTokenJson();
        body.refreshToken = refreshToken;

        const newTokens = await api.refreshToken(body);

        localStorage.setItem("accessToken", newTokens.accessToken);
        localStorage.setItem("refreshToken", newTokens.refreshToken);

        // refaz request
        newInit.headers = {
          ...(init?.headers ?? {}),
          Authorization: `Bearer ${newTokens.accessToken}`,
        };

        response = await fetch(input, newInit);
      } catch (error) {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("user");
        throw error;
      }
    }
  }

  if (!response.ok) {
    let json;

    try {
      json = await response.json();
    } catch {
      json = { message: response.statusText };
    }

    throw new ApiError(json.message, response.status, json);
  }

  return response;
}
