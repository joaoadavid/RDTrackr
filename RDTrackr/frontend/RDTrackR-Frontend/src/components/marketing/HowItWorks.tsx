import { UserPlus, Plug, Play, Replace } from "lucide-react";
import { Container } from "@/components/ui/container";

const steps = [
  {
    icon: UserPlus,
    title: "1. Cadastre-se",
    description:
      "Crie sua conta em menos de 2 minutos. Não precisa de cartão de crédito.",
  },
  {
    icon: Replace,
    title: "2. Cadastre seus produtos",
    description: "Integre facilmente com seus produtos existentes no estoques.",
  },
  {
    icon: Play,
    title: "3. Comece a operar",
    description:
      "Acesse seu dashboard e comece a gerenciar seu negócio imediatamente.",
  },
];

export function HowItWorks() {
  return (
    <section className="py-20 lg:py-32 bg-muted/30">
      <Container>
        <div className="text-center space-y-4 mb-16">
          <h2 className="text-3xl md:text-5xl font-bold tracking-tight">
            Como funciona
          </h2>
          <p className="text-xl text-muted-foreground max-w-2xl mx-auto">
            Comece a usar em três passos simples
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 lg:gap-12 max-w-5xl mx-auto">
          {steps.map((step, index) => {
            const Icon = step.icon;
            return (
              <div
                key={step.title}
                className="relative text-center animate-fade-in"
                style={{ animationDelay: `${index * 150}ms` }}
              >
                {/* Connector Line (hidden on mobile, last item) */}
                {index < steps.length - 1 && (
                  <div className="hidden md:block absolute top-12 left-1/2 w-full h-0.5 bg-border -z-10" />
                )}

                <div className="relative inline-flex items-center justify-center w-24 h-24 rounded-full bg-primary text-primary-foreground mb-6 shadow-lg">
                  <Icon className="w-10 h-10" />
                </div>

                <h3 className="text-xl font-semibold mb-3">{step.title}</h3>
                <p className="text-muted-foreground leading-relaxed">
                  {step.description}
                </p>
              </div>
            );
          })}
        </div>
      </Container>
    </section>
  );
}
