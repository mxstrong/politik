import { BsFillPersonFill, BsLink45Deg } from 'react-icons/bs';

import { IStatement } from '@type/api/statements';
import BackButton from '@element/BackButton';
import Tag from '@element/Tag';

interface IStatementPage {
  statement: IStatement;
}

const StatementPage: React.FC<IStatementPage> = ({ statement }) => {
  return (
    <div>
      <BackButton href="/" />
      <h1 className="text-2xl md:text-3xl font-semibold mt-6 mb-4 md:mt-8 md:mb-8">
        {statement.description}
      </h1>
      <div className="flex items-center space-x-2 mt-2 mb-5">
        <span className="w-6">
          <BsFillPersonFill className="w-6 h-6" />
        </span>
        <span className="font-bold">{statement.politician}</span>
      </div>
      <div className="flex items-center space-x-2 mb-5">
        <span className="w-6">
          <BsLink45Deg className="w-6 h-6" />
        </span>
        <a
          className="text-blue-500 truncate"
          href={statement.link}
          title={`${statement.politician} pareiškimas`}
        >
          {statement.link}
        </a>
      </div>
      <div className="flex flex-wrap">
        {statement.tags.map((tag) => (
          <Tag key={`tag-${tag}`}>{tag}</Tag>
        ))}
      </div>
    </div>
  );
};

export default StatementPage;
