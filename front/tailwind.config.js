const colors = require('tailwindcss/colors');

module.exports = {
  purge: [
    './pages/**/*.{js,ts,jsx,tsx}',
    './app/components/**/*.{js,ts,jsx,tsx}',
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
    colors: {
      ...colors,
      primary: '#457b9d',
      'primary-darker': '#3e6e8d',
      'primary-light': '#a8dadc',
      'primary-dark': '#1d3557',
      'primary-background': '#e1ebf1',
      secondary: '#e63946',
      white: '#fff',
      black: '#000',
      danger: '#f56565',
    },
  },
  variants: {
    extend: {
      borderRadius: ['first', 'last'],
      borderWidth: ['first', 'last'],
    },
  },
  plugins: [],
};
