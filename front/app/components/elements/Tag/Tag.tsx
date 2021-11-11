const Tag: React.FC = ({ children }) => {
  return (
    <div className="bg-primary-dark text-white rounded-full py-1 md:py-2 px-3 md:px-4 mr-2 mb-2 cursor-default">
      {children}
    </div>
  );
};

export default Tag;
