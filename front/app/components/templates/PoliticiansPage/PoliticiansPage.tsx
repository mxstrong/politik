import Button from '@element/Button';
import { BsPlusLg } from 'react-icons/bs';

import PoliticiansList from '@module/PoliticiansList';

const PoliticiansPage = () => {
  return (
    <div>
      <Button icon={<BsPlusLg />} variant="outlined">
        Pridėti naują
      </Button>
      <div className="my-6 md:my-12">
        <PoliticiansList />
      </div>
    </div>
  );
};

export default PoliticiansPage;
