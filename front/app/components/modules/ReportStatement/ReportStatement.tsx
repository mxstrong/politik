import { Formik, Form, FormikValues } from 'formik';
import * as yup from 'yup';
import { toast } from 'react-toastify';

import Modal from '@element/Modal';
import Button from '@element/Button';
import TextArea from '@element/TextArea';
import { IStatement } from '@type/api/statements';
import { _fetch } from '@util/fetch';
import { toastResponseErrorMessages } from '@util/errors';

interface IReport {
  statement: IStatement | undefined;
  onClose: () => void;
}

const VALIDATION_SCHEMA = yup.object({
  statementId: yup.string().required(),
  description: yup.string().min(1).required(),
});

const Report: React.FC<IReport> = ({ statement, onClose }) => {
  const getInitialValues = () => {
    return { statementId: statement?.statementId, description: '' };
  };

  const handleSubmit = async ({ statementId, description }: FormikValues) => {
    const reportRes = await _fetch({
      url: 'Reports',
      method: 'POST',
      data: { statementId, description },
    });

    if (!reportRes.error) {
      toast.success('Pranešimas išsiųstas sėkmingai.');
      onClose();
      return;
    }

    toastResponseErrorMessages(reportRes);
  };

  return (
    <Modal
      isOpen={!!statement}
      onClose={onClose}
      title="Pranešimas apie netinkamą pareiškimą"
    >
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
              <div className="space-y-2">
                <p className="font-semibold">Pareiškimo aprašymas:</p>
                <p className="">{statement?.description}</p>
              </div>

              <TextArea
                label="Pranešimas"
                name="description"
                value={values.description}
                onChange={handleChange}
                error={!!errors.description}
                minRows={3}
              />

              <div className="flex justify-end space-x-4 pt-10">
                <Button variant="outlined" onClick={onClose}>
                  Atšaukti
                </Button>
                <Button type="submit" disabled={isSubmitting || !dirty}>
                  Pranešti
                </Button>
              </div>
            </Form>
          );
        }}
      </Formik>
    </Modal>
  );
};

export default Report;
