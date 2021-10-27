import { ReactElement } from 'react';
import Head from 'next/head';

import { _fetch } from '@api/RestClient';
import DefaultLayout from '@layout/DefaultLayout';
import PoliticiansPage from '@template/PoliticiansPage';
import { NextPageWithLayout } from '@type/elements/app';

const PAGE_TITLE = 'Politikai';

const Politicians: NextPageWithLayout = () => {
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
