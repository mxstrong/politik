import { ParsedUrlQuery } from 'querystring';
import Head from 'next/head';
import { GetStaticProps } from 'next';
import { ReactElement } from 'react';

import DefaultLayout from '@layout/DefaultLayout';
import PoliticianPage from '@template/PoliticianPage';
import { _fetch } from '@util/fetch';
import { IPolitician } from '@type/api/politicians';
import { NextPageWithLayout } from '@type/elements/app';

interface IPoliticianProps {
  politician: IPolitician;
}

const Politician: NextPageWithLayout<IPoliticianProps> = ({ politician }) => {
  return (
    <div>
      <Head>
        <title>{politician.fullName}</title>
      </Head>

      <PoliticianPage politician={politician} />
    </div>
  );
};

Politician.getLayout = (page: ReactElement) => {
  return <DefaultLayout>{page}</DefaultLayout>;
};

export async function getStaticPaths() {
  const res = await _fetch({ url: 'Politicians' });

  if (!res.data) {
    return {
      notFound: true,
    };
  }

  const paths = res.data.map((item: IPolitician) => {
    return { params: { id: item.id } };
  });

  return {
    paths,
    fallback: 'blocking',
  };
}

interface IProps {
  politician: IPolitician;
}

interface IParams extends ParsedUrlQuery {
  id: string;
}

export const getStaticProps: GetStaticProps<IProps, IParams> = async ({
  params,
}) => {
  const res = await _fetch({ url: `Politicians/${params?.id}` });

  if (!res.data) {
    return {
      notFound: true,
    };
  }

  return {
    props: {
      politician: res.data,
    },
    revalidate: 60,
  };
};

export default Politician;
