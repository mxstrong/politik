const TextButton: React.FC<React.HTMLAttributes<HTMLButtonElement>> = ({
  children,

  ...rest
}) => {
  return (
    <button className="text-blue-400 hover:underline" {...rest}>
      {children}
    </button>
  );
};

export default TextButton;
