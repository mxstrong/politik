import { useState } from 'react';
import { BsPlusLg } from 'react-icons/bs';

import BackButton from '@element/BackButton';
import Button from '@element/Button';
import { IPolitician } from '@type/api/politicians';
import AddNewStatement from '@module/AddNewStatement';
import StatementsList from '@module/StatementsList';

interface IPoliticianPage {
  politician: IPolitician;
}

const PoliticianPage: React.FC<IPoliticianPage> = ({ politician }) => {
  const [isAddNewStatementOpen, setIsAddNewStatementOpen] = useState(false);

  return (
    <>
      <div>
        <BackButton href="/politicians" />
        <h1 className="text-2xl md:text-3xl font-semibold mt-6 mb-2 md:mt-8 md:mb-5">
          {politician.fullName}
        </h1>
        <h2 className="text-lg md:text-xl font-medium mb-4 md:mb-8 text-gray-500">
          {politician.party}
        </h2>
        <p>{politician.description}</p>
        <div className="my-12">
          <Button
            icon={<BsPlusLg />}
            onClick={() => setIsAddNewStatementOpen(true)}
          >
            Pridėti naują pareiškimą
          </Button>
        </div>
        <div className="my-6 md:my-12">
          <StatementsList politician={politician.id} />
        </div>
      </div>

      <AddNewStatement
        isOpen={isAddNewStatementOpen}
        onClose={() => setIsAddNewStatementOpen(false)}
        defaultValues={{
          politician: { label: politician.fullName, value: politician.id },
        }}
      />
    </>
  );
};

export default PoliticianPage;
