import { useState } from 'react';

import Button from '@element/Button';
import { BsPlusLg } from 'react-icons/bs';
import PoliticiansList from '@module/PoliticiansList';
import { IPoliticians } from '@type/api/politicians';
import AddNewPolitician from '@module/AddNewPolitician.tsx/AddNewPolitician';

const PoliticiansPage: React.FC<IPoliticians> = ({ politicians }) => {
  const [isAddPoliticianOpen, setIsAddPoliticianOpen] = useState(false);

  return (
    <>
      <div>
        <Button
          icon={<BsPlusLg />}
          variant="outlined"
          onClick={() => setIsAddPoliticianOpen(true)}
        >
          Pridėti naują
        </Button>
        <div className="my-6 md:my-12">
          <PoliticiansList politicians={politicians} />
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
