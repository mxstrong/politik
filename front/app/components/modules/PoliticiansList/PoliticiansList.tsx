import Link from 'next/link';
import { useRouter } from 'next/router';
import { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { _fetch } from '@api/RestClient';
import Spin from '@element/Spin';
import { fetchPoliticians } from '@redux/actions/politicians';
import { IPoliticiansListItem } from '@type/api/politicians';
import { AppState } from '@redux/reducers';

const PoliticiansList: React.FC = () => {
  const router = useRouter();
  const dispatch = useDispatch();

  const { politicians, loading } = useSelector(
    ({ politicians }: AppState) => politicians
  );

  useEffect(() => {
    dispatch(fetchPoliticians());
  }, []);

  if (loading) {
    return <Spin />;
  }

  return (
    <ul className="rounded shadow-lg divide-y">
      {politicians.map(({ id, fullName, party }: IPoliticiansListItem) => {
        return (
          <Link href={`${router.pathname}/${id}`} key={`politician-${id}`}>
            <li className=" bg-white p-4 first:rounded-t last:rounded-b cursor-pointer">
              <a className="font-bold text-lg text-black hover:underline">
                {fullName}
              </a>
              <div className="flex justify-between font-semibold text-sm text-coolGray-500 mt-2">
                <div className="font-normal">
                  {party ? party : 'Nepartinis'}
                </div>

                {/* <div className="font-normal">
                          Pareiškimų: <strong>{ @TODO: add statements count }</strong>
                        </div> */}
              </div>
            </li>
          </Link>
        );
      })}
    </ul>
  );
};

export default PoliticiansList;
