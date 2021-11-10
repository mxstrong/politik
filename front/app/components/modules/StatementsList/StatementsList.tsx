import Link from 'next/link';
import { useRouter } from 'next/router';
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

const STATEMENTS_FETCH_COUNT = 10;

interface IStatementsList {
  politician?: string;
}

interface IFilters {
  tags: ISelectOption[];
}

const StatementsList: React.FC<IStatementsList> = ({ politician }) => {
  const router = useRouter();
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
          pageNumber: 1,
          pageSize: STATEMENTS_FETCH_COUNT,
          politician,
          ...(filters?.tags.length && {
            tags: filters.tags
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
          pageNumber,
          pageSize: STATEMENTS_FETCH_COUNT,
          politician,
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
            pageNumber: 1,
            pageSize: STATEMENTS_FETCH_COUNT * pageNumber,
            politician,
          })
        );
        return;
      }
      toast.error('Įvyko klaida.');
    }
  };

  return (
    <div className="flex flex-col items-center">
      <div className="w-full mb-8">
        <Select
          options={tagOptions}
          label="Žyma"
          value={filters?.tags}
          onChange={(value: any) => setFilters({ ...filters, tags: value })}
          isMulti
          className="w-full"
        />
      </div>
      {loading && !statements.data.length ? (
        <Spin className="my-8" />
      ) : (
        <ul className="rounded shadow-lg divide-y mb-8 w-full">
          {statements.data.map(
            ({ statementId, description, politician }: IStatementListItem) => {
              return (
                <Link
                  href={`${router.pathname}${statementId}`}
                  key={`statement-${statementId}`}
                >
                  <li className=" bg-white p-4 first:rounded-t last:rounded-b cursor-pointer">
                    <div className="flex justify-between">
                      <a className="font-bold text-lg text-black hover:underline">
                        {description}
                      </a>
                      <IconButton
                        onClick={(e: React.MouseEvent<HTMLElement>) =>
                          handleStatementDelete(e, statementId)
                        }
                      >
                        <BsX className="w-6 h-6" />
                      </IconButton>{' '}
                    </div>

                    <div className="font-normal text-sm text-coolGray-500 mt-2">
                      {politician ? politician : 'Nepartinis'}
                    </div>
                  </li>
                </Link>
              );
            }
          )}
        </ul>
      )}

      {statements.HasNextPage && (
        <Button
          variant="outlined"
          onClick={() => setPageNumber((pageNumber) => pageNumber + 1)}
          loading={loading}
        >
          Daugiau...
        </Button>
      )}
    </div>
  );
};

export default StatementsList;
