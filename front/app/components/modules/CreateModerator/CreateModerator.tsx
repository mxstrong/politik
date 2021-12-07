import { Formik, Form, FormikValues } from 'formik';
import { useState } from 'react';
import * as yup from 'yup';

import Button from '@element/Button';
import Select from '@element/Select';

const VALIDATION_SCHEMA = yup.object({
  user: yup.string().required(),
});

const CreateModerator = () => {
  const [userOptions, setUserOptions] = useState([]);

  const getInitialValues = () => {
    return { user: null };
  };

  const handleSubmit = async ({ user }: FormikValues) => {
    console.log(user);
  };

  return (
    <Formik
      initialValues={getInitialValues()}
      validationSchema={VALIDATION_SCHEMA}
      onSubmit={handleSubmit}
      validateOnBlur={false}
      validateOnChange={false}
    >
      {({ values, errors, isSubmitting, dirty, setFieldValue }) => {
        return (
          <Form className="space-y-6" id="add-politician-form">
            <Select
              options={userOptions}
              label="Vartotojas, kuriam suteiksite moderatoriaus teises"
              value={values.user}
              onChange={(value) => setFieldValue('user', value)}
              error={!!errors.user}
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

export default CreateModerator;
