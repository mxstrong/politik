import BackButton from '@element/BackButton';
import { IPolitician } from '@type/api/politicians';

interface IPoliticianPage {
  politician: IPolitician;
}

const PoliticianPage: React.FC<IPoliticianPage> = ({ politician }) => {
  return (
    <div>
      <BackButton href="/politicians" />
      <h1 className="text-2xl md:text-3xl font-semibold mt-6 mb-2 md:mt-8 md:mb-5">
        {politician.fullName}
      </h1>
      <h2 className="text-lg md:text-xl font-medium mb-4 md:mb-8 text-gray-500">
        {politician.party}
      </h2>
      <p>{politician.description}</p>
    </div>
  );
};

export default PoliticianPage;
