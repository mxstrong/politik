import Head from 'next/head';
import { GetStaticProps, NextPage } from 'next';

import DefaultLayout from '@layout/DefaultLayout';
import { USER_TYPES } from 'constants/userTypes';
import CreateModeratorPage from '@template/CreateModeratorPage';

const PAGE_TITLE = 'Sukurti moderatorių';

const CreateModerator = () => {
  return (
    <div>
      <Head>
        <title>{PAGE_TITLE}</title>
      </Head>

      <CreateModeratorPage />
    </div>
  );
};

CreateModerator.getLayout = (page: NextPage) => {
  return <DefaultLayout title={PAGE_TITLE}>{page}</DefaultLayout>;
};

interface IProps {
  permissions: string[];
}

export const getStaticProps: GetStaticProps<IProps> = async () => {
  return {
    props: {
      permissions: [USER_TYPES.ADMINISTRATOR],
    },
  };
};

export default CreateModerator;
