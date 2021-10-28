import { ISelectOption } from '@type/elements/SelectOption';
import { GroupBase } from 'react-select';
import ReactCreatableSelect, { CreatableProps } from 'react-select/creatable';

interface ICreatableSelect
  extends CreatableProps<ISelectOption, true, GroupBase<ISelectOption>> {
  label?: string;
  error?: boolean;
  errorMessage?: string;
}

const CreatableSelect: React.FC<ICreatableSelect> = ({
  options,
  label,
  error,
  errorMessage,
  ...rest
}) => {
  return (
    <div>
      {label && (
        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
          {label}
        </label>
      )}
      <ReactCreatableSelect
        options={options}
        classNamePrefix={error ? 'react-select--danger' : 'react-select'}
        noOptionsMessage={() => 'Nėra pasirinkimų'}
        {...rest}
      />
      {error && (
        <p className="text-danger text-xs italic mt-1">
          {errorMessage ? errorMessage : 'Šis laukas yra privalomas.'}
        </p>
      )}
    </div>
  );
};

export default CreatableSelect;
