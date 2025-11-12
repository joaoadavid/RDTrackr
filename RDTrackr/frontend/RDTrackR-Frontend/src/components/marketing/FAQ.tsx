import { useState } from "react";
import { ChevronDown } from "lucide-react";
import { Container } from "@/components/ui/container";
import { cn } from "@/lib/utils";

const faqs = [
  {
    question: "O RDTrackr é gratuito?",
    answer:
      "Sim! O projeto é open-source e gratuito para uso. Você pode instalar, modificar e adaptar às necessidades da sua empresa sem custo.",
  },
  {
    question: "Para que tipo de empresa o RDTrackr é indicado?",
    answer:
      "O RDTrackr é ideal para empresas que desejam melhorar o controle de estoque, compras e movimentações internas, especialmente indústrias, distribuidoras e oficinas.",
  },
  {
    question: "Quais módulos estão disponíveis?",
    answer:
      "Atualmente, o sistema conta com: cadastro de produtos, fornecedores, depósitos, movimentação de estoque, pedidos de compra, reposição automatizada e relatórios operacionais.",
  },
  {
    question: "Posso integrar com outros sistemas?",
    answer:
      "Sim! O RDTrackr possui API REST estruturada, permitindo integração com ERPs, lojas virtuais, sistemas internos e automações.",
  },
  {
    question: "Meus dados estão seguros?",
    answer:
      "Sim! O sistema foi construído seguindo boas práticas de segurança, autenticação JWT, controle de permissões e tratamento de dados conforme a LGPD.",
  },
  {
    question: "Posso colaborar com o projeto?",
    answer:
      "Com certeza! Qualquer contribuição é bem-vinda: desenvolvimento, correção de bugs, documentação ou sugestões. O projeto está disponível no GitHub.",
  },
  {
    question: "Haverá suporte?",
    answer:
      "Por ser um projeto open-source, o suporte é comunitário. Porém, você pode abrir issues no GitHub ou entrar em contato para suporte profissional.",
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
