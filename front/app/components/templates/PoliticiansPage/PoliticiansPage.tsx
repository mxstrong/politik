import { useState } from 'react';
import { BsPlusLg } from 'react-icons/bs';

import Button from '@element/Button';
import PoliticiansList from '@module/PoliticiansList';
import AddNewPolitician from '@module/AddNewPolitician.tsx/AddNewPolitician';

const PoliticiansPage: React.FC = () => {
  const [isAddPoliticianOpen, setIsAddPoliticianOpen] = useState(false);

  return (
    <>
      <div>
        <Button
          icon={<BsPlusLg />}
          variant="outlined"
          onClick={() => setIsAddPoliticianOpen(true)}
        >
          Pridėti naują politiką
        </Button>
        <div className="my-6 md:my-12">
          <PoliticiansList />
        </div>
      </div>

      <AddNewPolitician
        isOpen={isAddPoliticianOpen}
        onClose={() => setIsAddPoliticianOpen(false)}
      />
    </>
  );
};

export default PoliticiansPage;
