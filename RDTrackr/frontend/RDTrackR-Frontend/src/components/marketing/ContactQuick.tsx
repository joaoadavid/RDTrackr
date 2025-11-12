import { Mail, BookOpen, MessageCircle } from "lucide-react";
import { Link } from "react-router-dom";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Container } from "@/components/ui/container";

const contactOptions = [
  {
    icon: Mail,
    title: "Email de Contato",
    description: "Retorno normalmente no mesmo dia",
    action: "joao.antoniodavid@hotmail.com",
    href: "mailto:joao.antoniodavid@hotmail.com",
    buttonText: "Enviar email",
  },
  {
    icon: BookOpen,
    title: "Repositório no GitHub",
    description: "Código aberto e documentação em evolução",
    action: "github.com/joaoadavid/RDTrackr",
    href: "https://github.com/joaoadavid/RDTrackr",
    buttonText: "Acessar GitHub",
  },
  {
    icon: MessageCircle,
    title: "Formulário de Contato",
    description: "Envie uma mensagem diretamente pelo site",
    action: "Formulário",
    href: "/contact",
    buttonText: "Abrir formulário",
  },
];

export function ContactQuick() {
  return (
    <section className="py-20 lg:py-32">
      <Container>
        <div className="text-center space-y-4 mb-16">
          <h2 className="text-3xl md:text-5xl font-bold tracking-tight">
            Entre em contato
          </h2>
          <p className="text-xl text-muted-foreground max-w-2xl mx-auto">
            Estamos aqui para ajudar. Escolha a melhor forma de nos contatar.
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 lg:gap-8 max-w-5xl mx-auto">
          {contactOptions.map((option, index) => {
            const Icon = option.icon;
            return (
              <Card
                key={option.title}
                className="text-center hover:shadow-lg transition-all duration-300 hover:border-primary/50 animate-fade-in"
                style={{ animationDelay: `${index * 100}ms` }}
              >
                <CardHeader>
                  <div className="w-16 h-16 rounded-full bg-primary/10 flex items-center justify-center mx-auto mb-4">
                    <Icon className="w-8 h-8 text-primary" />
                  </div>
                  <CardTitle className="text-xl">{option.title}</CardTitle>
                  <CardDescription className="text-base">
                    {option.description}
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-muted-foreground mb-4">{option.action}</p>
                  <Button variant="outline" asChild className="w-full">
                    <Link to={option.href}>{option.buttonText}</Link>
                  </Button>
                </CardContent>
              </Card>
            );
          })}
        </div>
      </Container>
    </section>
  );
}
