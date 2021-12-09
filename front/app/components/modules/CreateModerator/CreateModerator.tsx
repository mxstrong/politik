import { Formik, Form, FormikValues } from 'formik';
import * as yup from 'yup';
import { GroupBase, OptionsOrGroups } from 'react-select';
import { toast } from 'react-toastify';

import Button from '@element/Button';
import AsyncSelect from '@element/AsyncSelect';
import { _fetch } from '@util/fetch';
import { IUser } from '@type/api/user';

const VALIDATION_SCHEMA = yup.object({
  user: yup
    .object({ label: yup.string(), value: yup.string().required() })
    .required(),
});

const CreateModerator = () => {
  const getInitialValues = () => {
    return { user: null };
  };

  const handleSubmit = async ({ user }: FormikValues) => {
    const res = await _fetch({
      url: `Auth/AddModerator/${user?.value}`,
      method: 'POST',
    });

    if (res.error) {
      toast.error('Įvyko klaida.');
    } else {
      toast.success('Vartotojas sėkmingai paskirtas moderatoriumi.');
    }
  };

  const loadUserOptions = (
    inputValue: string,
    callback: (options: OptionsOrGroups<unknown, GroupBase<unknown>>) => void
  ) => {
    setTimeout(async () => {
      if (inputValue) {
        const res = await _fetch({
          url: 'Auth/GetUsers',
          params: { Search: inputValue },
        });

        if (!res.error) {
          const options = res.data.map(
            ({ userId, displayName, email }: IUser) => ({
              label: `${displayName} (${email})`,
              value: userId,
            })
          );

          callback(options);
        }
      }

      callback([]);
    }, 600);
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
          <Form className="space-y-6">
            <AsyncSelect
              label="Vartotojas, kuriam suteiksite moderatoriaus teises"
              value={values.user}
              onChange={(value) => setFieldValue('user', value)}
              loadOptions={loadUserOptions}
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
