import Link from 'next/link';
import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { _fetch } from '@util/fetch';
import Spin from '@element/Spin';
import { fetchPoliticians } from '@redux/actions/politicians';
import { IPoliticiansListItem } from '@type/api/politicians';
import { AppState } from '@redux/reducers';
import Button from '@element/Button';

export const POLITICIANS_FETCH_COUNT = 10;

const PoliticiansList: React.FC = () => {
  const dispatch = useDispatch();

  const [pageNumber, setPageNumber] = useState(1);

  const { politicians, loading } = useSelector(
    ({ politicians }: AppState) => politicians
  );

  useEffect(() => {
    dispatch(
      fetchPoliticians(
        {
          pageNumber,
          pageSize: POLITICIANS_FETCH_COUNT,
        },
        true
      )
    );
  }, [pageNumber]);

  return (
    <div className="flex flex-col items-center">
      {loading && !politicians.length ? (
        <Spin className="my-8" />
      ) : (
        <ul className="rounded shadow-lg divide-y w-full mb-8">
          {politicians.data.map(
            ({ id, fullName, party }: IPoliticiansListItem) => {
              return (
                <li
                  className=" bg-white p-4 first:rounded-t last:rounded-b cursor-pointer"
                  key={`politician-${id}`}
                >
                  <Link href={`politicians/${id}`}>
                    <a title={fullName}>
                      <span className="font-bold text-lg text-black hover:underline">
                        {fullName}
                      </span>
                      <div className="flex justify-between font-semibold text-sm text-coolGray-500 mt-2">
                        <div className="font-normal">
                          {party ? party : 'Nepartinis'}
                        </div>
                      </div>
                    </a>
                  </Link>
                </li>
              );
            }
          )}
        </ul>
      )}

      {politicians.HasNextPage && (
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

export default PoliticiansList;
