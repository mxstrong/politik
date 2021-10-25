import React, { ReactNode } from 'react';
import cx from 'classnames';
import TextareaAutosize, {
  TextareaAutosizeProps,
} from 'react-textarea-autosize';

interface ITextarea
  extends React.HTMLAttributes<HTMLTextAreaElement>,
    TextareaAutosizeProps {
  label?: string;
  error?: boolean;
  errorMessage?: string;
  name?: string;
  value?: string;
  onChange?: (e: React.ChangeEvent<any>) => void;
  children?: undefined;
  placeholder?: string;
}

const TextArea: React.FC<ITextarea> = ({
  label,
  className,
  minRows,
  maxRows,
  error = false,
  errorMessage = 'Šis laukas yra privalomas.',
  name,
  value,
  placeholder,
  onChange,
}) => {
  return (
    <div className="flex-1">
      {label && (
        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
          {label}
        </label>
      )}
      <TextareaAutosize
        className={cx(
          'appearance-none',
          'block',
          'w-full',
          'bg-gray-200',
          'text-gray-700',
          'border',
          'rounded py-3',
          'px-4',
          'leading-tight',
          'focus:outline-none',
          'focus:bg-white',
          'focus:border-gray-500',
          className,
          {
            'border-gray-200': !error,
            'border-danger': error,
          }
        )}
        minRows={minRows}
        maxRows={maxRows}
        name={name}
        value={value}
        placeholder={placeholder}
        onChange={onChange}
      />
      {error && (
        <p className="text-danger text-xs italic mt-1">{errorMessage}</p>
      )}
    </div>
  );
};

export default TextArea;
