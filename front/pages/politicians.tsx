import type { ReactElement } from 'react';
import Head from 'next/head';

import DefaultLayout from '@layout/DefaultLayout';

const Politicians = () => {
  return (
    <div>
      <Head>
        <title>Politikai</title>
      </Head>
      <h1>Politikai</h1>
    </div>
  );
};

Politicians.getLayout = (page: ReactElement) => {
  return <DefaultLayout>{page}</DefaultLayout>;
};

export default Politicians;
