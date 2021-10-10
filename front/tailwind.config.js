module.exports = {
  purge: [
    './pages/**/*.{js,ts,jsx,tsx}',
    './app/components/**/*.{js,ts,jsx,tsx}',
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
    colors: {
      primary: '#457b9d',
      'primary-light': '#a8dadc',
      'primary-dark': '#1d3557',
      secondary: '#e63946',
      background: '#f1faee',
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
};
