import { useRouter } from 'next/router';
import Head from 'next/head';
import { ReactElement } from 'react';

import DefaultLayout from '@layout/DefaultLayout';
import PoliticianPage from '@template/PoliticianPage';

const Politician = () => {
  const router = useRouter();
  const { pid } = router.query;

  const POLITICIAN = {
    id: pid,
    fullName: 'Ingrida Šimonytė',
    party: 'Tėvynės sąjunga - Lietuvos krikščionys demokratai',
    description:
      'Premjerė. Lietuvos ekonomistė, politikė, finansų ministrė (2009-2012 m.), Seimo narė ir 18-osios Vyriausybės Ministrė Pirmininkė. 1992 m. sidabro medaliu[2] baigė Vilniaus 7-ąją vidurinę mokyklą (dabar – Vilniaus Žirmūnų gimnazija). Iškart po mokyklos įstojo į Vilniaus universiteto Ekonomikos fakultetą. 1996 m. Šimonytė įgijo verslo administravimo ir vadybos bakalauro laipsnį, o 1998 m. – ekonomikos magistrą.',
  };

  return (
    <div>
      <Head>
        <title>{POLITICIAN.fullName}</title>
      </Head>

      <PoliticianPage politician={POLITICIAN} />
    </div>
  );
};

Politician.getLayout = (page: ReactElement) => {
  return <DefaultLayout>{page}</DefaultLayout>;
};

export default Politician;
