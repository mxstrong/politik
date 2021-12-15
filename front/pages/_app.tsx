import type { AppProps } from 'next/app';
import 'tailwindcss/tailwind.css';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Provider } from 'react-redux';

import { NextPageWithLayout } from '@type/elements/app';
import '../app/styles/app.scss';
import configureStore from '@redux/store';
import { parseLocalStorageItem } from '@util/storage';

type AppPropsWithLayout = AppProps & {
  Component: NextPageWithLayout;
};

export default function MyApp({ Component, pageProps }: AppPropsWithLayout) {
  const getLayout = Component.getLayout ?? ((page) => page);

  const currentUser = parseLocalStorageItem('currentUser');

  const isPermitted =
    !pageProps.permissions || pageProps.permissions.includes(currentUser?.role);

  if (isPermitted) {
    return getLayout(
      <>
        <Provider store={configureStore()}>
          <Component {...pageProps} />
        </Provider>
        <ToastContainer
          position="top-center"
          autoClose={5000}
          hideProgressBar
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
      </>
    );
  }

  return null;
}
