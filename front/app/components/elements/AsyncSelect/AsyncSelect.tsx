import ReactAsyncSelect from 'react-select/async';
import { GroupBase, OptionsOrGroups, Props as SelectProps } from 'react-select';

import { _fetch } from '@util/fetch';

interface IAsyncSelect extends SelectProps {
  loadOptions: (
    inputValue: string,
    callback: (options: OptionsOrGroups<unknown, GroupBase<unknown>>) => void
  ) => void;
  label?: string;
  error?: boolean;
  errorMessage?: string;
}

const AsyncSelect: React.FC<IAsyncSelect> = ({
  options,
  label,
  error,
  errorMessage,
  loadOptions,
  ...rest
}) => {
  return (
    <div>
      {label && (
        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
          {label}
        </label>
      )}
      <ReactAsyncSelect
        cacheOptions
        loadOptions={loadOptions}
        defaultOptions
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

export default AsyncSelect;
