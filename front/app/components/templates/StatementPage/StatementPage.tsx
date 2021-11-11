import { BsFillPersonFill, BsLink45Deg } from 'react-icons/bs';

import { IStatement } from '@type/api/statements';
import BackButton from '@element/BackButton';

interface IStatementPage {
  statement: IStatement;
}

const StatementPage: React.FC<IStatementPage> = ({ statement }) => {
  console.log(statement);
  return (
    <div>
      <BackButton href="/" />
      <h1 className="text-2xl md:text-3xl font-semibold mt-6 mb-4 md:mt-8 md:mb-8">
        {statement.description}
      </h1>
      <div className="flex items-center space-x-2 mb-5">
        <BsFillPersonFill className="w-5 h-5" />
        <span className="font-bold">{statement.politician}</span>
      </div>
      <div className="flex items-center space-x-2 mb-5">
        <BsLink45Deg className="w-8 h-8" />
        <a className="text-blue-500 break-all" href={statement.link}>
          {statement.link}
        </a>
      </div>
      <div className="flex space-x-2 flex-wrap">
        {statement.tags.map((tag) => (
          <>
            <div
              key={`tag-${tag}`}
              className="bg-primary-dark text-white rounded-full py-1 px-2 md:py-2 md:px-4"
            >
              {tag}
            </div>{' '}
            <div
              key={`tag-${tag}`}
              className="bg-primary-dark text-white rounded-full py-1 px-2 md:py-2 md:px-4"
            >
              {tag}
            </div>
          </>
        ))}
      </div>
    </div>
  );
};

export default StatementPage;
