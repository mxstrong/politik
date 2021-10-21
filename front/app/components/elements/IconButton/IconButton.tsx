import cx from 'classnames';

const IconButton: React.FC<React.HTMLAttributes<HTMLButtonElement>> = ({
  children,
  className,
  ...rest
}) => {
  return (
    <button
      className={cx(
        'outline-none',
        'flex',
        'items-center',
        'text-primary-dark',
        className
      )}
      {...rest}
    >
      {children}
    </button>
  );
};

export default IconButton;
