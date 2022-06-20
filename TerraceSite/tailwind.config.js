const production = !process.env.ROLLUP_WATCH;
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [],
  theme: {
    extend: {},
  },
  plugins: [],
  future: {
    purgeLayersByDefault: true,
    removeDeprecatedGapUtilities: true,
  },
  purge: {
    content: ["./src/**/*.svelte"],
    enabled: production, // disable purge in dev
  },
};
