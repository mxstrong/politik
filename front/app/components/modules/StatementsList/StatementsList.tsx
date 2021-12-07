import Link from 'next/link';
import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { BsX } from 'react-icons/bs';
import { toast } from 'react-toastify';

import { _fetch } from '@util/fetch';
import Spin from '@element/Spin';
import Button from '@element/Button';
import { fetchStatements } from '@redux/actions/statements';
import { IStatementListItem } from '@type/api/statements';
import { AppState } from '@redux/reducers';
import IconButton from '@element/IconButton';
import Select from '@element/Select';
import { ITag } from '@type/api/tags';
import { ISelectOption } from '@type/elements/SelectOption';
import Empty from '@element/Empty';
import { isMod } from '@util/general';

const STATEMENTS_FETCH_COUNT = 10;

interface IStatementsList {
  politician?: string;
}

interface IFilters {
  tags: ISelectOption[];
}

const StatementsList: React.FC<IStatementsList> = ({ politician }) => {
  const dispatch = useDispatch();

  const [pageNumber, setPageNumber] = useState(1);
  const [tagOptions, setTagOptions] = useState([]);
  const [filters, setFilters] = useState<IFilters>();

  const { statements, loading } = useSelector(
    ({ statements }: AppState) => statements
  );

  useEffect(() => {
    const fetchTags = async () => {
      const res = await _fetch({ url: 'Tags' });

      if (res.data) {
        const options = res.data.map(({ tagId, name }: ITag) => ({
          value: tagId,
          label: name,
        }));
        setTagOptions(options);
      }
    };

    fetchTags();
  }, []);

  useEffect(() => {
    if (filters) {
      dispatch(
        fetchStatements({
          PageNumber: 1,
          PageSize: STATEMENTS_FETCH_COUNT,
          Politician: politician,
          ...(filters?.tags.length && {
            Tags: filters.tags
              .map(({ value }: ISelectOption) => value)
              .join(','),
          }),
        })
      );
    }
  }, [filters]);

  useEffect(() => {
    dispatch(
      fetchStatements(
        {
          PageNumber: pageNumber,
          PageSize: STATEMENTS_FETCH_COUNT,
          Politician: politician,
        },
        true
      )
    );
  }, [pageNumber]);

  const handleStatementDelete = async (
    event: React.MouseEvent<HTMLElement>,
    statementId: string
  ) => {
    event.preventDefault();

    const shouldDelete = confirm('Ar tikrai norite ištrinti pareiškimą?');

    if (shouldDelete) {
      const res = await _fetch({
        url: `Statements/${statementId}`,
        method: 'DELETE',
      });

      if (!res.error) {
        toast.success('Pareiškimas ištrintas.');
        dispatch(
          fetchStatements({
            PageNumber: 1,
            PageSize: STATEMENTS_FETCH_COUNT * pageNumber,
            Politician: politician,
          })
        );
        return;
      }
      toast.error('Įvyko klaida.');
    }
  };

  return (
    <div className="flex flex-col items-center">
      {' '}
      <div className="w-full mb-8">
        <Select
          options={tagOptions}
          label="Žymos"
          value={filters?.tags}
          onChange={(value: any) => setFilters({ ...filters, tags: value })}
          isMulti
          className="w-full"
        />
      </div>
      {loading && !statements.data && <Spin className="my-8" />}
      {!loading && !!statements.data?.length && (
        <>
          <ul className="rounded shadow-lg divide-y mb-8 w-full">
            {statements.data?.map(
              ({
                statementId,
                description,
                politician,
              }: IStatementListItem) => {
                return (
                  <li
                    className="bg-white p-4 first:rounded-t last:rounded-b cursor-pointer"
                    key={`statement-${statementId}`}
                  >
                    <Link href={`/statements/${statementId}`}>
                      <a title={`${politician} pareiškimas`}>
                        <div className="flex justify-between">
                          <span className="font-bold text-lg text-black hover:underline">
                            {description}
                          </span>
                          {isMod() && (
                            <IconButton
                              onClick={(e: React.MouseEvent<HTMLElement>) =>
                                handleStatementDelete(e, statementId)
                              }
                            >
                              <BsX className="w-6 h-6" />
                            </IconButton>
                          )}
                        </div>
                        <div className="font-normal text-sm text-coolGray-500 mt-2">
                          {politician ? politician : 'Nepartinis'}
                        </div>
                      </a>
                    </Link>
                  </li>
                );
              }
            )}
          </ul>
          {statements.HasNextPage && (
            <Button
              variant="outlined"
              onClick={() => setPageNumber((pageNumber) => pageNumber + 1)}
              loading={loading}
            >
              Daugiau...
            </Button>
          )}
        </>
      )}
      {!loading && statements.data?.length === 0 && <Empty />}
    </div>
  );
};

export default StatementsList;
