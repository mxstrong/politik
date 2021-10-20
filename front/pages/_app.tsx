import type { AppProps } from 'next/app';
import 'tailwindcss/tailwind.css';

import { NextPageWithLayout } from '@type/elements/app';

type AppPropsWithLayout = AppProps & {
  Component: NextPageWithLayout;
};

export default function MyApp({ Component, pageProps }: AppPropsWithLayout) {
  const getLayout = Component.getLayout ?? ((page) => page);

  return getLayout(<Component {...pageProps} />);
}
