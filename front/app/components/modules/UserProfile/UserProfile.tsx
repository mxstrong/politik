import { Formik, Form } from 'formik';
import * as yup from 'yup';

import Input from '@element/Input';
import Button from '@element/Button';

const VALIDATION_SCHEMA = yup.object({
  nickname: yup.string().min(1).required(),
});

const UserProfile = () => {
  const getInitialValues = () => {
    return { nickname: '' };
  };

  const handleSubmit = () => {};

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
          <Form className="space-y-6" id="add-politician-form">
            <Input
              label="Vartotojo vardas"
              placeholder="Vartotojo vardas..."
              name="nickname"
              value={values.nickname}
              onChange={handleChange}
              error={!!errors.nickname}
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
