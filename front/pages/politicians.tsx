import type { ReactElement } from 'react';
import Head from 'next/head';

import DefaultLayout from '@layout/DefaultLayout';
import PoliticiansPage from '@template/PoliticiansPage';

const PAGE_TITLE = 'Politikai';

const Politicians = () => {
  return (
    <div>
      <Head>
        <title>{PAGE_TITLE}</title>
      </Head>

      <PoliticiansPage />
    </div>
  );
};

Politicians.getLayout = (page: ReactElement) => {
  return <DefaultLayout title={PAGE_TITLE}>{page}</DefaultLayout>;
};

export default Politicians;
