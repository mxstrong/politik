import { useState } from 'react';
import { BsPlusLg, BsFillPencilFill } from 'react-icons/bs';

import BackButton from '@element/BackButton';
import Button from '@element/Button';
import { IPolitician } from '@type/api/politicians';
import AddNewStatement from '@module/AddNewStatement';
import StatementsList from '@module/StatementsList';
import PoliticianForm from '@module/PoliticianForm';
import { isMod } from '@util/general';

interface IPoliticianPage {
  politician: IPolitician;
}

const PoliticianPage: React.FC<IPoliticianPage> = ({ politician }) => {
  const [isAddNewStatementOpen, setIsAddNewStatementOpen] = useState(false);
  const [isEditPolititianOpen, setIsEditPoliticianOpen] = useState(false);

  return (
    <>
      <div>
        <BackButton href="/politicians" />
        <h1 className="text-2xl md:text-3xl font-semibold mt-6 mb-2 md:mt-8 md:mb-5">
          {politician.fullName}
        </h1>
        <h3 className="text-base md:text-lg font-medium mb-4 md:mb-8 text-gray-500">
          {politician.party?.longName}
        </h3>
        <p>{politician.description}</p>
        <div className="my-12 flex flex-col space-y-2">
          {isMod() && (
            <Button
              icon={<BsFillPencilFill />}
              variant="outlined"
              className="w-max"
              onClick={() => setIsEditPoliticianOpen(true)}
            >
              Redaguoti
            </Button>
          )}
          <Button
            icon={<BsPlusLg />}
            onClick={() => setIsAddNewStatementOpen(true)}
            className="w-max"
          >
            Pridėti naują pareiškimą
          </Button>
        </div>
        <div className="my-6 md:my-12">
          <h2 className="text-xl md:text-2xl font-semibold mt-6 mb-2 md:mt-8 md:mb-5 border-b border-black pb-3">
            Pareiškimai
          </h2>
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

      <PoliticianForm
        isOpen={isEditPolititianOpen}
        onClose={() => setIsEditPoliticianOpen(false)}
        initialValues={politician}
      />
    </>
  );
};

export default PoliticianPage;
