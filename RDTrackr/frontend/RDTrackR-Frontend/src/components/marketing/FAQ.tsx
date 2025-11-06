import { useState } from "react";
import { ChevronDown } from "lucide-react";
import { Container } from "@/components/ui/container";
import { cn } from "@/lib/utils";

const faqs = [
  {
    question: "Qual é o preço do plano?",
    answer:
      "Oferecemos planos flexíveis que se adaptam ao tamanho do seu negócio. Entre em contato para conhecer as opções e escolher a melhor para você.",
  },
  {
    question: "Meus dados estão seguros?",
    answer:
      "Sim! Utilizamos criptografia de ponta a ponta, backups automáticos e seguimos as melhores práticas de segurança da indústria (incluindo LGPD).",
  },
  {
    question: "Posso integrar com outros sistemas?",
    answer:
      "Sim, oferecemos uma API REST completa que permite integração com ERPs, e-commerce, sistemas de pagamento e muito mais.",
  },
  {
    question: "Como funciona o suporte?",
    answer:
      "Oferecemos suporte por email, chat e documentação completa. Clientes premium têm acesso a suporte prioritário com SLA garantido.",
  },
  {
    question: "Posso cancelar a qualquer momento?",
    answer:
      "Sim, não há período de fidelidade. Você pode cancelar sua assinatura a qualquer momento sem custos adicionais.",
  },
  {
    question: "Existe período de teste gratuito?",
    answer:
      "Sim! Você pode testar todas as funcionalidades por 14 dias sem precisar cadastrar cartão de crédito.",
  },
];

export function FAQ() {
  const [openIndex, setOpenIndex] = useState<number | null>(0);

  const toggleFAQ = (index: number) => {
    setOpenIndex(openIndex === index ? null : index);
  };

  return (
    <section className="py-20 lg:py-32">
      <Container>
        <div className="text-center space-y-4 mb-16">
          <h2 className="text-3xl md:text-5xl font-bold tracking-tight">
            Perguntas frequentes
          </h2>
          <p className="text-xl text-muted-foreground max-w-2xl mx-auto">
            Tire suas dúvidas sobre o sistema
          </p>
        </div>

        <div className="max-w-3xl mx-auto space-y-4">
          {faqs.map((faq, index) => (
            <div
              key={index}
              className="border border-border rounded-lg overflow-hidden transition-all duration-300 hover:border-primary/50"
            >
              <button
                onClick={() => toggleFAQ(index)}
                className="w-full flex items-center justify-between p-6 text-left bg-card hover:bg-muted/30 transition-colors"
                aria-expanded={openIndex === index}
                aria-controls={`faq-answer-${index}`}
              >
                <span className="font-semibold text-lg pr-4">{faq.question}</span>
                <ChevronDown
                  className={cn(
                    "w-5 h-5 text-muted-foreground transition-transform duration-300 flex-shrink-0",
                    openIndex === index && "rotate-180"
                  )}
                />
              </button>
              <div
                id={`faq-answer-${index}`}
                className={cn(
                  "overflow-hidden transition-all duration-300",
                  openIndex === index ? "max-h-96 opacity-100" : "max-h-0 opacity-0"
                )}
              >
                <div className="p-6 pt-0 text-muted-foreground leading-relaxed">
                  {faq.answer}
                </div>
              </div>
            </div>
          ))}
        </div>
      </Container>
    </section>
  );
}
