import { Formik, Form, FormikValues } from 'formik';
import { toast } from 'react-toastify';
import * as yup from 'yup';

import Modal from '@element/Modal';
import Button from '@element/Button';
import Input from '@element/Input';
import { _fetch } from '@util/fetch';
import { fetchUser } from '@util/fetches';
import { setLocalStorageItem } from '@util/storage';

interface ILogin {
  isOpen: boolean;
  onClose: () => void;
}

const VALIDATION_SCHEMA = yup.object({
  email: yup.string().email().required(),
  password: yup.string().min(1).required(),
});

const Login: React.FC<ILogin> = ({ isOpen, onClose }) => {
  const getInitialValues = () => {
    return {
      email: '',
      password: '',
    };
  };

  const handleSubmit = async ({ email, password }: FormikValues) => {
    const data = { email, password };

    const res = await _fetch({ url: 'Auth/Login', method: 'POST', data });

    if (!res.error) {
      setLocalStorageItem('jwt', `${res.data.jwt}`);
      const userRes = await fetchUser();
      if (!userRes?.error) {
        toast.success('Prisijungimas sėkmingas.');
        onClose();
        return;
      }
    }

    toast.error('Įvyko klaida.');
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Prisijungimas">
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
                  Prisijungti
                </Button>
              </div>
            </Form>
          );
        }}
      </Formik>
    </Modal>
  );
};

export default Login;
