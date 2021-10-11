import type { ReactElement } from 'react';
import Head from 'next/head';

import DefaultLayout from '@layout/DefaultLayout';

const PAGE_TITLE = 'Naujausi pareiškimai';

const Index = () => {
  return (
    <div>
      <Head>
        <title>{PAGE_TITLE}</title>
      </Head>
    </div>
  );
};

Index.getLayout = (page: ReactElement) => {
  return <DefaultLayout title={PAGE_TITLE}>{page}</DefaultLayout>;
};

export default Index;
