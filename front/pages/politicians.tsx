import { ReactElement, useEffect } from 'react';
import Head from 'next/head';
import { GetStaticProps } from 'next';
import RestClient from '@api/RestClient';

import DefaultLayout from '@layout/DefaultLayout';
import PoliticiansPage from '@template/PoliticiansPage';
import { NextPageWithLayout } from '@type/elements/app';
import { IPoliticians } from '@type/api/politicians';

const PAGE_TITLE = 'Politikai';

const Politicians: NextPageWithLayout<IPoliticians> = ({ politicians }) => {
  return (
    <div>
      <Head>
        <title>{PAGE_TITLE}</title>
      </Head>

      <PoliticiansPage politicians={politicians} />
    </div>
  );
};

Politicians.getLayout = (page: ReactElement) => {
  return <DefaultLayout title={PAGE_TITLE}>{page}</DefaultLayout>;
};

export const getStaticProps: GetStaticProps = async () => {
  const res = await RestClient.get('Politicians');

  if (!res.data) {
    return {
      notFound: true,
    };
  }

  return {
    props: { politicians: res.data },
  };
};

export default Politicians;
