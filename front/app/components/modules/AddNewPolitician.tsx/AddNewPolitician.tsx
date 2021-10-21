import { Formik, Form, FormikValues } from 'formik';

import Button from '@element/Button';
import Input from '@element/Input';
import Modal from '@element/Modal';
import TextArea from '@element/TextArea';
import { fetch } from '@api/RestClient';

interface IAddNewPolitician {
  isOpen: boolean;
  onClose: () => void;
}

const AddNewPolitician: React.FC<IAddNewPolitician> = ({ isOpen, onClose }) => {
  const getInitialValues = () => {
    return {
      firstName: '',
      lastName: '',
      description: '',
    };
  };

  const handleSubmit = async ({
    firstName,
    lastName,
    description,
  }: FormikValues) => {
    const data = {
      party: null,
      fullName: `${firstName} ${lastName}`,
      description,
    };

    const res = await fetch({ url: 'Politicians', method: 'POST', data });

    if (!res.error) {
      onClose();
      return;
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Naujo politiko pridėjimas">
      <Formik initialValues={getInitialValues()} onSubmit={handleSubmit}>
        {({ values, handleChange }) => {
          return (
            <Form className="space-y-6" id="add-politician-form">
              <div className="flex flex-wrap space-y-6 md:space-y-0 flex-col md:space-x-4 md:flex-row justify-between">
                <Input
                  label="Vardas"
                  placeholder="Politiko vardas..."
                  name="firstName"
                  value={values.firstName}
                  onChange={handleChange}
                />
                <Input
                  label="Pavardė"
                  placeholder="Politiko pavardė..."
                  name="lastName"
                  value={values.lastName}
                  onChange={handleChange}
                />
              </div>
              <TextArea
                label="Aprašymas"
                minRows={6}
                maxRows={12}
                name="description"
                value={values.description}
                onChange={handleChange}
              />
              <div className="flex justify-end space-x-4 pt-10">
                <Button variant="outlined" onClick={onClose}>
                  Atšaukti
                </Button>
                <Button type="submit">Siųsti</Button>
              </div>
            </Form>
          );
        }}
      </Formik>
    </Modal>
  );
};

export default AddNewPolitician;
