import Button from '@element/Button';
import { BsPlusLg } from 'react-icons/bs';
import PoliticiansList from '@module/PoliticiansList';
import { IPoliticians } from '@type/api/politicians';

const PoliticiansPage: React.FC<IPoliticians> = ({ politicians }) => {
  return (
    <div>
      <Button icon={<BsPlusLg />} variant="outlined">
        Pridėti naują
      </Button>
      <div className="my-6 md:my-12">
        <PoliticiansList politicians={politicians} />
      </div>
    </div>
  );
};

export default PoliticiansPage;
