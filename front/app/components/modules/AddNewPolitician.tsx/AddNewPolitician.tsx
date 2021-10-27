import { useEffect, useState } from 'react';
import { Formik, Form, FormikValues } from 'formik';
import { toast } from 'react-toastify';
import * as yup from 'yup';

import Button from '@element/Button';
import Input from '@element/Input';
import Modal from '@element/Modal';
import TextArea from '@element/TextArea';
import { _fetch } from '@api/RestClient';
import Select from '@element/Select';
import { ISelectOption } from '@type/elements/SelectOption';
import { IParty } from '@type/api/parties';

const VALIDATION_SCHEMA = yup.object({
  firstName: yup.string().min(1).required(),
  lastName: yup.string().min(1).required(),
  party: yup.object({ value: yup.string() }).nullable(),
  description: yup.string().min(1).required(),
});

interface IAddNewPolitician {
  isOpen: boolean;
  onClose: () => void;
}

const AddNewPolitician: React.FC<IAddNewPolitician> = ({ isOpen, onClose }) => {
  const [partyOptions, setPartyOptions] = useState<ISelectOption[]>([]);

  useEffect(() => {
    const fetchParties = async () => {
      const res = await _fetch({ url: 'Parties' });

      if (res.data) {
        const newPartyOptions = res.data.map((party: IParty) => ({
          label: `${party.longName} (${party.shortName})`,
          value: party.id,
        }));
        const withoutPartyOption = { label: '— (nepartinis)', value: null };

        setPartyOptions([withoutPartyOption, ...newPartyOptions]);
      }
    };

    if (isOpen) {
      fetchParties();
    }
  }, [isOpen]);

  const getInitialValues = () => {
    return {
      firstName: '',
      lastName: '',
      party: null,
      description: '',
    };
  };

  const handleSubmit = async ({
    firstName,
    lastName,
    party,
    description,
  }: FormikValues) => {
    const data = {
      firstName,
      lastName,
      partyId: party.value || null,
      description,
    };

    const res = await _fetch({ url: 'Politicians', method: 'POST', data });

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
        {({ values, handleChange, errors, isSubmitting, setFieldValue }) => {
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
              <Select
                options={partyOptions}
                label="Partija"
                value={values.party}
                onChange={(value) => setFieldValue('party', value)}
                error={!!errors.party}
              />
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
