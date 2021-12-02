import { useState } from 'react';
import { BsPlusLg } from 'react-icons/bs';

import Button from '@element/Button';
import PoliticiansList from '@module/PoliticiansList';
import PoliticianForm from '@module/PoliticianForm/PoliticianForm';

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

      <PoliticianForm
        isOpen={isAddPoliticianOpen}
        onClose={() => setIsAddPoliticianOpen(false)}
      />
    </>
  );
};

export default PoliticiansPage;
