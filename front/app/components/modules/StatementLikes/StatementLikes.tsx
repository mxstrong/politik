import { useState, useEffect } from 'react';

import TextButton from '@element/TextButton';
import { fetchAll, _fetch } from '@util/fetch';
import { parseLocalStorageItem } from '@util/storage';

interface IStatementLikesProps {
  statementId: string;
}

const StatementLikes: React.FC<IStatementLikesProps> = ({ statementId }) => {
  const currentUser = parseLocalStorageItem('currentUser');
  const [hasLiked, setHasLiked] = useState(false);
  const [likeCount, setLikeCount] = useState();

  const fetchData = async () => {
    const [likeCountRes, hasLikedRes] = await fetchAll([
      _fetch({ url: `Statements/likeCount/${statementId}` }),
      _fetch({ url: `Statements/hasLiked/${statementId}` }),
    ]);

    if (!likeCountRes.error) {
      setLikeCount(likeCountRes.data);
    }

    if (!hasLikedRes.error) {
      setHasLiked(hasLikedRes.data);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleLike = async () => {
    const likeRes = await _fetch({
      url: `Statements/${hasLiked ? 'unlike' : 'like'}/${statementId}`,
      method: 'POST',
    });

    if (!likeRes.error) {
      fetchData();
    }
  };

  return (
    <div>
      {currentUser && (
        <TextButton onClick={handleLike}>
          {hasLiked ? 'Pažymėti, kad nepatinka' : 'Pažymėti, kad patinka'}
        </TextButton>
      )}
      {typeof likeCount === 'number' && (
        <div>
          Teigiamų įvertinimų: <strong>{likeCount}</strong>
        </div>
      )}
    </div>
  );
};

export default StatementLikes;
