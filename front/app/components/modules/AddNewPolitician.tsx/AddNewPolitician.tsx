import { Formik, Form, FormikValues } from 'formik';
import { toast } from 'react-toastify';
import * as yup from 'yup';

import Button from '@element/Button';
import Input from '@element/Input';
import Modal from '@element/Modal';
import TextArea from '@element/TextArea';
import { fetch } from '@api/RestClient';

const VALIDATION_SCHEMA = yup.object({
  firstName: yup.string().min(1).required(),
  lastName: yup.string().min(1).required(),
  description: yup.string().min(1).required(),
});

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
      toast.success('Informacija išsiųsta.');
      onClose();
      return;
    }

    toast.error('Įvyko klaida.');
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Naujo politiko pridėjimas">
      <Formik
        initialValues={getInitialValues()}
        validationSchema={VALIDATION_SCHEMA}
        onSubmit={handleSubmit}
        validateOnBlur={false}
        validateOnChange={false}
      >
        {({ values, handleChange, errors, isSubmitting }) => {
          return (
            <Form className="space-y-6" id="add-politician-form">
              <div className="flex flex-wrap space-y-6 md:space-y-0 flex-col md:space-x-4 md:flex-row justify-between">
                <Input
                  label="Vardas"
                  placeholder="Politiko vardas..."
                  name="firstName"
                  value={values.firstName}
                  onChange={handleChange}
                  error={!!errors.firstName}
                />
                <Input
                  label="Pavardė"
                  placeholder="Politiko pavardė..."
                  name="lastName"
                  value={values.lastName}
                  onChange={handleChange}
                  error={!!errors.lastName}
                />
              </div>
              <TextArea
                label="Aprašymas"
                minRows={6}
                maxRows={12}
                name="description"
                value={values.description}
                onChange={handleChange}
                error={!!errors.description}
                placeholder="Politiko aprašymas..."
              />
              <div className="flex justify-end space-x-4 pt-10">
                <Button variant="outlined" onClick={onClose}>
                  Atšaukti
                </Button>
                <Button type="submit" disabled={isSubmitting}>
                  Siųsti
                </Button>
              </div>
            </Form>
          );
        }}
      </Formik>
    </Modal>
  );
};

export default AddNewPolitician;
