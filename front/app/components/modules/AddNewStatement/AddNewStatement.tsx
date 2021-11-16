import { useEffect, useState } from 'react';
import { Formik, Form, FormikValues } from 'formik';
import { toast } from 'react-toastify';
import * as yup from 'yup';

import Button from '@element/Button';
import Input from '@element/Input';
import Modal from '@element/Modal';
import TextArea from '@element/TextArea';
import { fetchAll, _fetch } from '@util/fetch';
import Select from '@element/Select';
import { ISelectOption } from '@type/elements/SelectOption';
import { IPolitician } from '@type/api/politicians';
import { ITag } from '@type/api/tags';
import CreatableSelect from '@element/CreatableSelect';

interface IDefaultValues {
  politician?: ISelectOption;
}

interface IAddNewStatement {
  isOpen: boolean;
  onClose: () => void;
  defaultValues?: IDefaultValues;
}

const VALIDATION_SCHEMA = yup.object({
  politician: yup.object({ value: yup.string() }).required(),
  link: yup.string().min(1).required(),
  description: yup.string().min(1).required(),
  tags: yup
    .array()
    .min(1)
    .of(yup.object({ value: yup.string().nullable() }))
    .required(),
});

const AddNewStatement: React.FC<IAddNewStatement> = ({
  isOpen,
  onClose,
  defaultValues = {},
}) => {
  const [politicianOptions, setPoliticianOptions] = useState<ISelectOption[]>(
    []
  );
  const [tagOptions, setTagOptions] = useState<ISelectOption[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const [politicians, tags] = await fetchAll([
        _fetch({ url: 'Politicians' }),
        _fetch({ url: 'Tags' }),
      ]);

      if (politicians.data) {
        const newPoliticianOptions = politicians.data.map(
          (politician: IPolitician) => ({
            label: politician.fullName,
            value: politician.id,
          })
        );

        setPoliticianOptions(newPoliticianOptions);
      }

      if (tags.data) {
        const newTagOptions = tags.data.map((tag: ITag) => ({
          label: tag.name,
          value: tag.tagId,
        }));

        setTagOptions(newTagOptions);
      }
    };

    if (isOpen) {
      fetchData();
    }
  }, [isOpen]);

  const getInitialValues = () => {
    return {
      politician: defaultValues.politician || null,
      link: '',
      description: '',
      tags: [],
    };
  };

  const handleSubmit = async ({
    politician,
    link,
    description,
    tags,
  }: FormikValues) => {
    const data = {
      politicianId: politician.value,
      link,
      description,
      tags: tags.map(({ value, label }: ISelectOption) => ({
        tagId: value,
        name: label,
      })),
    };

    const res = await _fetch({ url: 'Statements', method: 'POST', data });

    if (!res.error) {
      toast.success('Informacija išsiųsta.');
      onClose();
      return;
    }

    toast.error('Įvyko klaida.');
  };

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}
      title="Naujo pareiškimo pridėjimas"
    >
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
              <Select
                options={politicianOptions}
                label="Politikas"
                value={values.politician}
                onChange={(value) => setFieldValue('politician', value)}
                error={!!errors.politician}
              />
              <Input
                label="Nuoroda į šaltinį"
                placeholder="https://..."
                name="link"
                value={values.link}
                onChange={handleChange}
                error={!!errors.link}
              />
              <CreatableSelect
                options={tagOptions}
                label="Žymos"
                value={values.tags}
                onChange={(value) => setFieldValue('tags', value)}
                onCreateOption={(value) =>
                  setFieldValue('tags', [
                    ...values.tags,
                    { label: value, value: null },
                  ])
                }
                error={!!errors.tags}
                isMulti
                formatCreateLabel={(input) => `Sukurti naują žymą "${input}"`}
              />
              <TextArea
                label="Aprašymas"
                minRows={6}
                maxRows={12}
                name="description"
                value={values.description}
                onChange={handleChange}
                error={!!errors.description}
                placeholder="Pareiškimo aprašymas..."
              />
              <div className="flex justify-end space-x-4 pt-10">
                <Button variant="outlined" onClick={onClose}>
                  Atšaukti
                </Button>
                <Button type="submit" disabled={isSubmitting}>
                  Pridėti
                </Button>
              </div>
            </Form>
          );
        }}
      </Formik>
    </Modal>
  );
};

export default AddNewStatement;
