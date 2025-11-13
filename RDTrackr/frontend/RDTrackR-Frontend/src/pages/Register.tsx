import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { ArrowLeft } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import LogoR from "@/assets/LogoRDTrackR.svg";
import { useToast } from "@/hooks/use-toast";
import { useAuth } from "@/context/AuthContext";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

export default function Register() {
  const { register } = useAuth();
  const navigate = useNavigate();
  const { toast } = useToast();

  const [formData, setFormData] = useState({
    nome: "",
    email: "",
    senha: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await register(formData.nome, formData.email, formData.senha);

      toast({
        title: "Conta criada com sucesso!",
        description: `Bem-vindo, ${formData.nome}!`,
      });

      navigate("/dashboard");
    } catch (error: any) {
      const msg =
        error?.result?.message ??
        error?.data?.message ??
        "Erro ao registrar usuário.";

      toast({
        title: "Erro no cadastro",
        description: msg,
        variant: "destructive",
      });
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-background via-muted/30 to-background p-4">
      <div className="max-w-md mx-auto space-y-6">
        <div className="text-center">
          <Link to="/" className="inline-flex items-center gap-2 text-sm mb-8">
            <ArrowLeft className="w-4 h-4" />
            Voltar para home
          </Link>
        </div>

        <Card>
          <CardHeader className="text-center">
            <img src={LogoR} className="h-20 scale-125" />
            <CardTitle className="text-2xl">Criar conta</CardTitle>
            <CardDescription>
              Preencha as informações para criar sua conta
            </CardDescription>
          </CardHeader>

          <CardContent>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <Label htmlFor="nome">Nome</Label>
                <Input id="nome" name="nome" onChange={handleChange} required />
              </div>

              <div>
                <Label htmlFor="email">E-mail</Label>
                <Input
                  id="email"
                  name="email"
                  type="email"
                  onChange={handleChange}
                  required
                />
              </div>

              <div>
                <Label htmlFor="senha">Senha</Label>
                <Input
                  id="senha"
                  name="senha"
                  type="password"
                  onChange={handleChange}
                  required
                />
              </div>

              <Button type="submit" className="w-full">
                Criar conta
              </Button>

              <Button asChild variant="outline" className="w-full">
                <Link to="/login">Já tenho conta</Link>
              </Button>
            </form>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
