import { useNavigate } from "react-router-dom";
import { checkSession } from "@/lib/session";
import { toast } from "@/hooks/use-toast";

export function useEnterApp() {
  const navigate = useNavigate();

  const enterApp = async () => {
    try {
      const hasSession = await checkSession();

      if (hasSession) {
        navigate("/");
      } else {
        navigate("/login");
      }
    } catch (error) {
      console.error("Error checking session:", error);
      toast({
        title: "Erro",
        description: "Não foi possível verificar sua sessão. Tente novamente.",
        variant: "destructive",
      });
    }
  };

  return { enterApp };
}
