import { Formik, Form, FormikValues } from 'formik';
import * as yup from 'yup';
import { toast } from 'react-toastify';

import Input from '@element/Input';
import Button from '@element/Button';
import { parseLocalStorageItem } from '@util/storage';
import { _fetch } from '@util/fetch';
import { fetchUser } from '@util/fetches';

const VALIDATION_SCHEMA = yup.object({
  nickname: yup.string().min(1).required(),
  email: yup.string().email().required(),
  oldPassword: yup.string().when('newPassword', {
    is: (newPassword: string) => newPassword?.length > 0,
    then: yup.string().required(),
  }),
  newPassword: yup.string(),
  passwordConfirmation: yup.string().when('newPassword', {
    is: (newPassword: string) => newPassword?.length > 0,
    then: yup
      .string()
      .oneOf([yup.ref('newPassword'), ''])
      .required('Slaptažodžiai privalo sutapti'),
  }),
});

const UserProfile = () => {
  const getInitialValues = () => {
    const initialUserData = parseLocalStorageItem('currentUser');

    return {
      nickname: initialUserData?.displayName,
      oldPassword: '',
      newPassword: '',
      passwordConfirmation: '',
      email: initialUserData?.email,
    };
  };

  const handleSubmit = async ({
    nickname,
    email,
    oldPassword,
    newPassword,
  }: FormikValues) => {
    const initialUserData = parseLocalStorageItem('currentUser');

    if (nickname && nickname !== initialUserData.displayName) {
      const nicknameRes = await _fetch({
        url: 'Auth/ChangeDisplayName',
        method: 'POST',
        data: { userId: initialUserData.userId, newDisplayName: nickname },
      });

      if (!nicknameRes.error) {
        toast.success('Vartotojo vardas pakeistas sėkmingai.');
      } else {
        toast.error('Įvyko vartotojo vardo keitimo klaida.');
      }
    }

    if (email && email !== initialUserData.email) {
      const nicknameRes = await _fetch({
        url: 'Auth/ChangeEmailName',
        method: 'POST',
        data: { userId: initialUserData.userId, newEmail: email },
      });

      if (!nicknameRes.error) {
        toast.success('El. paštas pakeistas sėkmingai.');
      } else {
        toast.error('Įvyko el. pašto keitimo klaida.');
      }
    }

    if (oldPassword && newPassword) {
      const passwordRes = await _fetch({
        url: 'Auth/ChangePassword',
        method: 'POST',
        data: { userId: initialUserData.userId, oldPassword, newPassword },
      });

      if (!passwordRes.error) {
        toast.success('Slaptažodis pakeistas sėkmingai.');
      } else {
        toast.error('Įvyko slaptažodžio keitimo klaida.');
      }
    }

    fetchUser();
  };

  return (
    <Formik
      initialValues={getInitialValues()}
      validationSchema={VALIDATION_SCHEMA}
      onSubmit={handleSubmit}
      validateOnBlur={false}
      validateOnChange={false}
    >
      {({ values, handleChange, errors, isSubmitting, dirty }) => {
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
              label="Senas slaptažodis"
              name="oldPassword"
              type="password"
              value={values.oldPassword}
              onChange={handleChange}
              error={!!errors.oldPassword}
            />
            <Input
              label="Naujas slaptažodis"
              name="newPassword"
              type="password"
              value={values.newPassword}
              onChange={handleChange}
              error={!!errors.newPassword}
            />
            <Input
              label="Pakartokite naują slaptažodį"
              name="passwordConfirmation"
              type="password"
              value={values.passwordConfirmation}
              onChange={handleChange}
              error={!!errors.passwordConfirmation}
              errorMessage={errors.passwordConfirmation}
            />
            <div className="flex justify-end space-x-4 pt-10">
              <Button type="submit" disabled={isSubmitting || !dirty}>
                Išsaugoti
              </Button>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
};

export default UserProfile;
