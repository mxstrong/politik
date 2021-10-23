import Header from '@module/Header';

interface IDefaultLayout {
  title?: string;
}

const DefaultLayout: React.FC<IDefaultLayout> = ({ title, children }) => {
  return (
    <div className="bg-coolGray-100 min-h-screen">
      <Header />
      <main className="max-w-3xl mx-auto px-6 md:px-8 py-8 md:py-12">
        {title && (
          <h1 className="text-3xl md:text-5xl font-semibold mb-6 md:mb-12">
            {title}
          </h1>
        )}
        {children}
      </main>
    </div>
  );
};

export default DefaultLayout;
