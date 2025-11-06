import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Menu, X, Moon, Sun } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Container } from "@/components/ui/container";
import { useTheme } from "@/hooks/use-theme";
import { useEnterApp } from "@/hooks/useEnterApp";
import LogoRDTrackR from "@/assets/LogoRDTrackR.svg";

export function Header() {
  const [isScrolled, setIsScrolled] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const { theme, toggleTheme } = useTheme();
  const { enterApp } = useEnterApp();

  useEffect(() => {
    const handleScroll = () => setIsScrolled(window.scrollY > 20);
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  const navLinks = [
    { href: "/replenishment", label: "Planejamento" },
    { href: "/contact", label: "Contato" },
    { href: "/support", label: "Suporte" },
  ];

  return (
    <header
      className={`fixed top-0 left-0 right-0 z-50 transition-all duration-300 ${
        isScrolled
          ? "bg-background/80 backdrop-blur-lg border-b border-border shadow-sm"
          : "bg-transparent"
      }`}
    >
      <Container>
        <nav className="flex items-center justify-between h-16 lg:h-20">
          <Link to="/" className="flex items-center gap-3 shrink-0">
            <img
              src={LogoRDTrackR}
              alt="RDTrackR Logo"
              className="h-16 w-auto sm:h-20 md:h-24 object-contain -ml-1"
            />
          </Link>
          {/* Desktop Navigation */}
          <div className="hidden lg:flex items-center space-x-1">
            {navLinks.map((link) => (
              <Button key={link.href} variant="ghost" asChild>
                <Link to={link.href}>{link.label}</Link>
              </Button>
            ))}
          </div>
          {/* Desktop Actions */}
          <div className="hidden lg:flex items-center space-x-3">
            <Button
              variant="ghost"
              size="icon"
              onClick={toggleTheme}
              aria-label="Alternar tema"
            >
              {theme === "light" ? (
                <Moon className="w-5 h-5" />
              ) : (
                <Sun className="w-5 h-5" />
              )}
            </Button>

            {/* Botões principais */}
            <div className="flex items-center space-x-2">
              <Button asChild variant="default" size="lg">
                <Link to="/register">Criar conta</Link>
              </Button>
              <Button onClick={enterApp} variant="default" size="lg">
                Entrar
              </Button>
            </div>
          </div>
          {/* Mobile Menu Button */}
          <div className="flex lg:hidden items-center space-x-2">
            <Button
              variant="ghost"
              size="icon"
              onClick={toggleTheme}
              aria-label="Alternar tema"
            >
              {theme === "light" ? (
                <Moon className="w-5 h-5" />
              ) : (
                <Sun className="w-5 h-5" />
              )}
            </Button>
            <Button
              variant="ghost"
              size="icon"
              onClick={() => setIsMobileMenuOpen(!isMobileMenuOpen)}
              aria-label="Menu"
            >
              {isMobileMenuOpen ? (
                <X className="w-6 h-6" />
              ) : (
                <Menu className="w-6 h-6" />
              )}
            </Button>
          </div>
        </nav>

        {/* Mobile Menu */}
        {isMobileMenuOpen && (
          <div className="lg:hidden py-4 border-t border-border/60 border-r-8 border-primary/70 bg-background/70 backdrop-blur-md rounded-b-2xl animate-fade-in">
            <div className="flex flex-col space-y-2">
              {navLinks.map((link) => (
                <Button
                  key={link.href}
                  variant="ghost"
                  className="justify-start"
                  asChild
                  onClick={() => setIsMobileMenuOpen(false)}
                >
                  <Link to={link.href}>{link.label}</Link>
                </Button>
              ))}
              <Button
                asChild
                variant="default"
                className="w-full"
                onClick={() => setIsMobileMenuOpen(false)}
              >
                <Link to="/register">Criar conta</Link>
              </Button>
              <Button
                onClick={() => {
                  enterApp();
                  setIsMobileMenuOpen(false);
                }}
                variant="default"
                className="w-full"
              >
                Entrar
              </Button>
            </div>
          </div>
        )}
      </Container>
    </header>
  );
}
