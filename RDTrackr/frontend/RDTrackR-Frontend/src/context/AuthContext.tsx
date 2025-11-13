import { createContext, useContext, useEffect, useState } from "react";
import { api } from "@/lib/api";
import {
  RequestLoginJson,
  RequestRegisterUserJson,
} from "@/generated/apiClient";

interface AuthContextProps {
  isAuthenticated: boolean;
  user: string | null;
  login: (email: string, password: string) => Promise<void>;
  register: (name: string, email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextProps | null>(null);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<string | null>(null);

  // Recupera sessão no carregamento inicial
  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) setUser(storedUser);
  }, []);

  // -----------------------------
  // LOGIN
  // -----------------------------
  const login = async (email: string, password: string) => {
    const body = new RequestLoginJson();
    body.email = email;
    body.password = password;

    const result = await api.login(body);

    localStorage.setItem("accessToken", result.tokens?.accessToken ?? "");
    localStorage.setItem("refreshToken", result.tokens?.refreshToken ?? "");
    localStorage.setItem("user", result.name ?? "");

    setUser(result.name ?? "");
  };

  // -----------------------------
  // REGISTER
  // -----------------------------
  const register = async (name: string, email: string, password: string) => {
    const body = new RequestRegisterUserJson();
    body.name = name;
    body.email = email;
    body.password = password;

    const result = await api.userPOST(body);

    localStorage.setItem("accessToken", result.tokens?.accessToken ?? "");
    localStorage.setItem("refreshToken", result.tokens?.refreshToken ?? "");
    localStorage.setItem("user", result.name ?? "");

    setUser(result.name ?? "");
  };

  // -----------------------------
  // LOGOUT
  // -----------------------------
  const logout = async () => {
    try {
      const refresh = localStorage.getItem("refreshToken");
      if (refresh) await api.logout(refresh);
    } catch {
      // refresh inválido, ignora
    }

    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("user");

    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated: !!user,
        user,
        login,
        register,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth must be inside AuthProvider");
  return ctx;
}
