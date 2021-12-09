import { ParsedUrlQuery } from 'querystring';
import Head from 'next/head';
import { GetStaticProps } from 'next';
import { ReactElement } from 'react';

import DefaultLayout from '@layout/DefaultLayout';
import { _fetch } from '@util/fetch';
import { NextPageWithLayout } from '@type/elements/app';
import { IStatement } from '@type/api/statements';
import StatementsPage from '@template/StatementPage';

interface IStatementsProps {
  statement: IStatement;
}

const Statement: NextPageWithLayout<IStatementsProps> = ({ statement }) => {
  return (
    <div>
      <Head>
        <title>{statement.description}</title>
      </Head>

      <StatementsPage statement={statement} />
    </div>
  );
};

Statement.getLayout = (page: ReactElement) => {
  return <DefaultLayout>{page}</DefaultLayout>;
};

export async function getStaticPaths() {
  const res = await _fetch({ url: 'Statements' });

  if (!res.data) {
    return {
      notFound: true,
    };
  }

  const paths = res.data.map((item: IStatement) => {
    return { params: { id: item.statementId } };
  });

  return {
    paths,
    fallback: 'blocking',
  };
}

interface IProps {
  statement: IStatement;
}

interface IParams extends ParsedUrlQuery {
  id: string;
}

export const getStaticProps: GetStaticProps<IProps, IParams> = async ({
  params,
}) => {
  const res = await _fetch({ url: `Statements/${params?.id}` });

  if (!res.data) {
    return {
      notFound: true,
    };
  }

  return {
    props: {
      statement: res.data,
    },
    revalidate: 60,
  };
};

export default Statement;
