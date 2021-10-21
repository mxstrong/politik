import React, { ReactNode } from 'react';
import cx from 'classnames';
import TextareaAutosize, {
  TextareaAutosizeProps,
} from 'react-textarea-autosize';

interface ITextarea extends TextareaAutosizeProps {
  label?: string;
}

const TextArea: React.FC<ITextarea> = ({
  label,
  className,
  minRows,
  maxRows,
}) => {
  const textAreaElement = (
    <TextareaAutosize
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
      minRows={minRows}
      maxRows={maxRows}
    />
  );

  if (label) {
    return (
      <div className="flex-1">
        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2">
          {label}
        </label>
        {textAreaElement}
      </div>
    );
  }

  return textAreaElement;
};

export default TextArea;
