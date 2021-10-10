import Header from '@module/Header';

const BasicLayout: React.FC = ({ children }) => {
  return (
    <div>
      <Header />
      <main className="my-0 mx-auto p-8">{children}</main>
    </div>
  );
};

export default BasicLayout;
