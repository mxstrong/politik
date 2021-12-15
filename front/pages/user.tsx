import Head from 'next/head';
import { GetStaticProps, NextPage } from 'next';

import DefaultLayout from '@layout/DefaultLayout';
import UserPage from '@template/UserPage';
import { getLocalStorageItem } from '@util/storage';
import { USER_TYPES } from 'constants/userTypes';

const PAGE_TITLE = 'Vartotojo profilis';

const User = () => {
  return (
    <div>
      <Head>
        <title>{PAGE_TITLE}</title>
      </Head>

      <UserPage />
    </div>
  );
};

User.getLayout = (page: NextPage) => {
  return <DefaultLayout title={PAGE_TITLE}>{page}</DefaultLayout>;
};

interface IProps {
  permissions: string[];
}

export const getStaticProps: GetStaticProps<IProps> = async () => {
  return {
    props: {
      permissions: [
        USER_TYPES.USER,
        USER_TYPES.MODERATOR,
        USER_TYPES.ADMINISTRATOR,
      ],
    },
  };
};

export default User;
