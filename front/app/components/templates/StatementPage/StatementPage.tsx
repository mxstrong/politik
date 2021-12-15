import { BsFillPersonFill, BsLink45Deg } from 'react-icons/bs';
import { LinkPreview } from '@dhaiwat10/react-link-preview';

import { IStatement } from '@type/api/statements';
import BackButton from '@element/BackButton';
import Tag from '@element/Tag';
import StatementLikes from '@module/StatementLikes';

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
        <LinkPreview url={statement.link} width="400px" />
      </div>
      <div className="flex flex-wrap">
        {statement.tags.map((tag) => (
          <Tag key={`tag-${tag}`}>{tag}</Tag>
        ))}
      </div>
      <div className="mt-4">
        <StatementLikes statementId={statement.statementId} />
      </div>
    </div>
  );
};

export default StatementPage;
