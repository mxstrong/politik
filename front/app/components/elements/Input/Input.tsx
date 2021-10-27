import React from 'react';
import cx from 'classnames';

interface IInput extends React.HTMLAttributes<HTMLInputElement> {
  type?: 'text' | 'password' | 'email';
  label?: string;
  name?: string;
  value?: string;
  error?: boolean;
  errorMessage?: string;
}

const Input: React.FC<IInput> = ({
  type = 'text',
  label,
  className,
  name,
  value,
  error,
  errorMessage,
  ...rest
}) => {
  return (
    <div className="flex-1">
      {label && (
        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
          {label}
        </label>
      )}
      <input
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
        type={type}
        name={name}
        value={value}
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

export default Input;
