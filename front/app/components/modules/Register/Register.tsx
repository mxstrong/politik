import { Formik, Form, FormikValues } from 'formik';
import { toast } from 'react-toastify';
import * as yup from 'yup';

import Modal from '@element/Modal';
import Button from '@element/Button';
import Input from '@element/Input';
import { _fetch } from '@util/fetch';

interface IRegister {
  isOpen: boolean;
  onClose: () => void;
}

const VALIDATION_SCHEMA = yup.object({
  nickname: yup.string().min(1).required(),
  email: yup.string().email().required(),
  password: yup.string().min(1).required(),
});

const Register: React.FC<IRegister> = ({ isOpen, onClose }) => {
  const getInitialValues = () => {
    return {
      nickname: '',
      email: '',
      password: '',
    };
  };

  const handleSubmit = async ({ nickname, email, password }: FormikValues) => {
    const data = { displayName: nickname, email, password };

    const res = await _fetch({ url: 'Auth/Register', method: 'POST', data });

    if (!res.error) {
      toast.success('Registracija sėkminga.');
      onClose();

      return;
    }

    toast.error('Įvyko klaida.');
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Registracija">
      <Formik
        initialValues={getInitialValues()}
        validationSchema={VALIDATION_SCHEMA}
        onSubmit={handleSubmit}
        validateOnBlur={false}
        validateOnChange={false}
      >
        {({ values, handleChange, errors, isSubmitting, setFieldValue }) => {
          return (
            <Form className="space-y-6">
              <Input
                label="Vartotojo vardas"
                placeholder="Vartotojo vardas..."
                name="nickname"
                value={values.nickname}
                onChange={handleChange}
                error={!!errors.nickname}
              />
              <Input
                label="El. paštas"
                placeholder="El. paštas..."
                name="email"
                value={values.email}
                onChange={handleChange}
                error={!!errors.email}
              />
              <Input
                label="Slaptažodis"
                name="password"
                type="password"
                value={values.password}
                onChange={handleChange}
                error={!!errors.password}
              />
              <div className="flex justify-end space-x-4 pt-10">
                <Button variant="outlined" onClick={onClose}>
                  Atšaukti
                </Button>
                <Button type="submit" disabled={isSubmitting}>
                  Registruotis
                </Button>
              </div>
            </Form>
          );
        }}
      </Formik>
    </Modal>
  );
};

export default Register;
