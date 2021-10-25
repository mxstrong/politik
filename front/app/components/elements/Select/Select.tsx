import ReactSelect, { Props as SelectProps } from 'react-select';

interface ISelect extends SelectProps {
  label?: string;
  error?: boolean;
  errorMessage?: string;
  // onChange: (newValue: any) => void; // https://github.com/DefinitelyTyped/DefinitelyTyped/issues/32553
}

const Select: React.FC<ISelect> = ({
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
      <ReactSelect
        options={options}
        classNamePrefix={error ? 'react-select--danger' : 'react-select'}
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

export default Select;
