/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: "class",

  // Onde o Tailwind vai procurar classes
  content: [
    "./**/templates/**/*.html",
    "./base_templates/**/*.html",
    "./**/*.{js,ts}", // scripts (toggle, menus, etc.)
    "./project/**/*.py", // se renderiza classes em views/templatetags
    "./project/static_src/**/*.{html,js,ts}", // ajuste para sua pasta de origem
  ],

  // Classes que o purge não pode remover
  safelist: [
    "h-[calc(100vh-4rem)]",
    "transition-[margin]",
    "lg:ml-64",
    "-translate-x-full",
    "lg:translate-x-0",
  ],

  theme: {
    extend: {
      colors: {
        primary: {
          50: "#eff6ff",
          100: "#dbeafe",
          200: "#bfdbfe",
          300: "#93c5fd",
          400: "#60a5fa",
          500: "#3b82f6",
          600: "#2563eb",
          700: "#1d4ed8",
          800: "#1e40af",
          900: "#1e3a8a",
          950: "#172554",
        },
      },
      fontFamily: {
        sans: [
          "Inter",
          "ui-sans-serif",
          "system-ui",
          "-apple-system",
          "Segoe UI",
          "Roboto",
          "Helvetica Neue",
          "Arial",
          "Noto Sans",
          "sans-serif",
          "Apple Color Emoji",
          "Segoe UI Emoji",
          "Segoe UI Symbol",
          "Noto Color Emoji",
        ],
        body: [
          "Inter",
          "ui-sans-serif",
          "system-ui",
          "-apple-system",
          "Segoe UI",
          "Roboto",
          "Helvetica Neue",
          "Arial",
          "Noto Sans",
          "sans-serif",
          "Apple Color Emoji",
          "Segoe UI Emoji",
          "Segoe UI Symbol",
          "Noto Color Emoji",
        ],
      },
      // Tipografia bonita no claro/escuro (opcional)
      typography: ({ theme }) => ({
        DEFAULT: {
          css: {
            color: theme("colors.gray.700"),
            a: {
              color: theme("colors.emerald.600"),
              "&:hover": { color: theme("colors.emerald.700") },
            },
            h1: { color: theme("colors.gray.900") },
            h2: { color: theme("colors.gray.900") },
          },
        },
        invert: {
          css: {
            color: theme("colors.gray.300"),
            a: {
              color: theme("colors.emerald.400"),
              "&:hover": { color: theme("colors.emerald.300") },
            },
            h1: { color: theme("colors.white") },
            h2: { color: theme("colors.white") },
          },
        },
      }),
    },
  },

  plugins: [require("daisyui"), require("@tailwindcss/typography")],

  daisyui: {
    themes: ["light", "dark", "corporate"],
    darkTheme: "dark",
  },
};
