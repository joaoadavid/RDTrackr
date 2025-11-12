import { Link } from "react-router-dom";
import { Mail, Github, ExternalLink } from "lucide-react";
import { Container } from "@/components/ui/container";
import { Separator } from "@/components/ui/separator";
import LogoRDTrackR from "@/assets/LogoR.svg";
export function Footer() {
  return (
    <footer className="border-t bg-white dark:bg-gray-950">
      <Container className="py-12">
        <div className="grid grid-cols-1 gap-10 sm:grid-cols-3">        
          <div>
            <div className="flex items-center gap-2">
            <img
              src={LogoRDTrackR}
              alt="RDTrackR Logo"
              className="h-10 w-auto object-contain sm:h-9 md:h-10"
            />
          </div>
            <p className="mt-3 text-sm text-muted-foreground">
              Gestão inteligente de estoque e compras para pequenas e médias indústrias.
            </p>
          </div>

          {/* Navegação */}
          <div>
            <h4 className="text-sm font-semibold mb-3">Navegação</h4>
            <ul className="space-y-2 text-sm">
              <li>
                <Link to="/" className="hover:underline">
                  Início
                </Link>
              </li>
              <li>
                <Link to="/login" className="hover:underline">
                  Entrar
                </Link>
              </li>
              <li>
                <Link to="/contact" className="hover:underline">
                  Contato
                </Link>
              </li>
            </ul>
          </div>

          {/* Contato / Social */}
          <div>
            <h4 className="text-sm font-semibold mb-3">Contato</h4>
            <ul className="space-y-3 text-sm">
              <li className="flex items-center gap-2">
                <Mail size={16} />
                <a
                  href="mailto:joao.antoniodavid@hotmail.com"
                  className="hover:underline"
                >
                  joao.antoniodavid@hotmail.com
                </a>
              </li>

              <li className="flex items-center gap-2">
                <Github size={16} />
                <a
                  href="https://github.com/joaoadavid/RDTrackr"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="flex items-center gap-1 hover:underline"
                >
                  GitHub <ExternalLink size={14} />
                </a>
              </li>
            </ul>
          </div>
        </div>

        <Separator className="my-10" />

        <p className="text-center text-xs text-muted-foreground">
          © {new Date().getFullYear()} RDTrackr — Projeto acadêmico desenvolvido por João David.
        </p>
      </Container>
    </footer>
  );
}
