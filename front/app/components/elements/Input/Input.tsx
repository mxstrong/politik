import React from 'react';
import cx from 'classnames';
import { FieldProps } from 'formik';

interface IInput extends React.HTMLAttributes<HTMLInputElement> {
  type?: 'text' | 'password' | 'email';
  label?: string;
  name?: string;
  value?: string;
}

const Input: React.FC<IInput> = ({
  type = 'text',
  label,
  className,
  name,
  value,
  ...rest
}) => {
  const inputElement = (
    <input
      className={cx(
        'appearance-none',
        'block',
        'w-full',
        'bg-gray-200',
        'text-gray-700',
        'border',
        'border-gray-200',
        'rounded py-3',
        'px-4',
        'leading-tight',
        'focus:outline-none',
        'focus:bg-white',
        'focus:border-gray-500',
        className
      )}
      type={type}
      name={name}
      value={value}
      {...rest}
    />
  );

  if (label) {
    return (
      <div className="flex-1">
        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
          {label}
        </label>
        {inputElement}
      </div>
    );
  }

  return inputElement;
};

export default Input;
