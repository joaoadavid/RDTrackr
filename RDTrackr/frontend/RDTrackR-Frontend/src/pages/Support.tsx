import { Header } from "@/components/marketing/Header";
import { Footer } from "@/components/marketing/Footer";
import { Container } from "@/components/ui/container";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import {
  HelpCircle,
  Book,
  Users,
  Activity,
  MessageCircle,
  ExternalLink,
} from "lucide-react";
import { Link } from "react-router-dom";

const supportTopics = [
  {
    title: "Como começar?",
    description: "Guia passo a passo para criar sua conta e começar a usar",
    icon: HelpCircle,
  },
  {
    title: "Integrações com API",
    description: "Aprenda a conectar sistemas externos",
    icon: Book,
  },
  {
    title: "Gerenciar usuários",
    description: "Controle de acesso e permissões",
    icon: Users,
  },
  {
    title: "Relatórios e métricas",
    description: "Entenda seus dados e dashboards",
    icon: Activity,
  },
];

export default function Support() {
  return (
    <div className="min-h-screen flex flex-col">
      <Header />
      <main className="flex-1 pt-24 pb-20">
        <Container>
          <div className="max-w-5xl mx-auto space-y-12">
            {/* Header */}
            <div className="text-center space-y-4 animate-fade-in-up">
              <Badge variant="secondary" className="mb-2">
                Central de Ajuda
              </Badge>
              <h1 className="text-4xl md:text-5xl font-bold tracking-tight">
                Como podemos ajudar?
              </h1>
              <p className="text-xl text-muted-foreground max-w-2xl mx-auto">
                Encontre respostas, tutoriais e abra chamados de suporte
              </p>
            </div>

            {/* Quick Actions */}
            <div className="grid md:grid-cols-3 gap-6 animate-fade-in">
              <Card className="text-center hover:shadow-lg transition-all hover:border-primary/50">
                <CardHeader>
                  <div className="w-16 h-16 rounded-full bg-green-500/10 flex items-center justify-center mx-auto mb-4">
                    <Activity className="w-8 h-8 text-green-500" />
                  </div>
                  <CardTitle>Status do Sistema</CardTitle>
                  <CardDescription>
                    Verifique o status dos serviços
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <Badge variant="secondary" className="bg-green-500/10 text-green-700 dark:text-green-400">
                    Todos os sistemas operacionais
                  </Badge>
                  <Button variant="outline" className="w-full mt-4" asChild>
                    <a href="#" target="_blank" rel="noopener noreferrer">
                      Ver detalhes
                      <ExternalLink className="ml-2 w-4 h-4" />
                    </a>
                  </Button>
                </CardContent>
              </Card>

              <Card className="text-center hover:shadow-lg transition-all hover:border-primary/50">
                <CardHeader>
                  <div className="w-16 h-16 rounded-full bg-primary/10 flex items-center justify-center mx-auto mb-4">
                    <Book className="w-8 h-8 text-primary" />
                  </div>
                  <CardTitle>Documentação</CardTitle>
                  <CardDescription>
                    Guias completos e referências de API
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-muted-foreground mb-4">
                    Mais de 100 artigos e tutoriais
                  </p>
                  <Button variant="outline" className="w-full" asChild>
                    <a href="#" target="_blank" rel="noopener noreferrer">
                      Acessar docs
                      <ExternalLink className="ml-2 w-4 h-4" />
                    </a>
                  </Button>
                </CardContent>
              </Card>

              <Card className="text-center hover:shadow-lg transition-all hover:border-primary/50">
                <CardHeader>
                  <div className="w-16 h-16 rounded-full bg-accent/10 flex items-center justify-center mx-auto mb-4">
                    <Users className="w-8 h-8 text-accent" />
                  </div>
                  <CardTitle>Comunidade</CardTitle>
                  <CardDescription>
                    Conecte-se com outros usuários
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-muted-foreground mb-4">
                    Compartilhe dicas e soluções
                  </p>
                  <Button variant="outline" className="w-full" asChild>
                    <a href="#" target="_blank" rel="noopener noreferrer">
                      Entrar na comunidade
                      <ExternalLink className="ml-2 w-4 h-4" />
                    </a>
                  </Button>
                </CardContent>
              </Card>
            </div>

            {/* Popular Topics */}
            <div className="space-y-6 animate-fade-in">
              <h2 className="text-3xl font-bold text-center">Tópicos populares</h2>
              <div className="grid md:grid-cols-2 gap-4">
                {supportTopics.map((topic, index) => {
                  const Icon = topic.icon;
                  return (
                    <Card
                      key={topic.title}
                      className="hover:shadow-md transition-all hover:border-primary/50 cursor-pointer"
                      style={{ animationDelay: `${index * 100}ms` }}
                    >
                      <CardHeader className="flex-row items-center gap-4 space-y-0">
                        <div className="w-12 h-12 rounded-lg bg-primary/10 flex items-center justify-center">
                          <Icon className="w-6 h-6 text-primary" />
                        </div>
                        <div>
                          <CardTitle className="text-lg">{topic.title}</CardTitle>
                          <CardDescription>{topic.description}</CardDescription>
                        </div>
                      </CardHeader>
                    </Card>
                  );
                })}
              </div>
            </div>

            {/* Open Ticket CTA */}
            <Card className="bg-gradient-to-br from-primary/10 to-accent/10 border-primary/20 animate-fade-in">
              <CardHeader className="text-center">
                <MessageCircle className="w-12 h-12 text-primary mx-auto mb-4" />
                <CardTitle className="text-2xl">Não encontrou o que procura?</CardTitle>
                <CardDescription className="text-base">
                  Nossa equipe de suporte está pronta para ajudar
                </CardDescription>
              </CardHeader>
              <CardContent className="flex flex-col sm:flex-row gap-4 justify-center">
                <Button variant="hero" size="lg" asChild>
                  <Link to="/contact">Abrir chamado</Link>
                </Button>
                <Button variant="outline-white" size="lg" asChild>
                  <a href="mailto:suporte@seusaas.com">Enviar email</a>
                </Button>
              </CardContent>
            </Card>
          </div>
        </Container>
      </main>
      <Footer />
    </div>
  );
}
