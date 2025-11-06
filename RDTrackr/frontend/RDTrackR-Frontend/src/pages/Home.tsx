import { Header } from "@/components/marketing/Header";
import { Hero } from "@/components/marketing/Hero";
import { FeatureGrid } from "@/components/marketing/FeatureGrid";
import { HowItWorks } from "@/components/marketing/HowItWorks";
import { FAQ } from "@/components/marketing/FAQ";
import { CTA } from "@/components/marketing/CTA";
import { ContactQuick } from "@/components/marketing/ContactQuick";
import { Footer } from "@/components/marketing/Footer";

export default function Home() {
  return (
    <div className="min-h-screen">
      <Header />
      <main>
        <Hero />
        <FeatureGrid />
        <HowItWorks />
        <FAQ />
        <CTA />
        <ContactQuick />
      </main>
      <Footer />
    </div>
  );
}
