const IconButton: React.FC<React.HTMLAttributes<HTMLButtonElement>> = ({
  children,
  ...rest
}) => {
  return (
    <button className="outline-none flex items-center" {...rest}>
      {children}
    </button>
  );
};

export default IconButton;
