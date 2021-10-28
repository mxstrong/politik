import { useState } from 'react';
import { BsPlusLg } from 'react-icons/bs';

import Button from '@element/Button';
import AddNewStatement from '@module/AddNewStatement';

const NewestStatementsPage = () => {
  const [isAddNewStatementOpen, setIsAddNewStatementOpen] = useState(false);

  return (
    <>
      <div>
        <Button
          icon={<BsPlusLg />}
          onClick={() => setIsAddNewStatementOpen(true)}
        >
          Pridėti naują pareiškimą
        </Button>
      </div>

      <AddNewStatement
        isOpen={isAddNewStatementOpen}
        onClose={() => setIsAddNewStatementOpen(false)}
      />
    </>
  );
};

export default NewestStatementsPage;
