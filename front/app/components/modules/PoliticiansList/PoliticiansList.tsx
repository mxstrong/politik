import Link from 'next/link';
import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { _fetch } from '@util/fetch';
import Spin from '@element/Spin';
import { fetchPoliticians } from '@redux/actions/politicians';
import { IPoliticiansListItem } from '@type/api/politicians';
import { AppState } from '@redux/reducers';
import Button from '@element/Button';
import Input from '@element/Input';
import { ISelectOption } from '@type/elements/SelectOption';
import { IParty } from '@type/api/parties';
import Select from '@element/Select';

export const POLITICIANS_FETCH_COUNT = 10;

interface IFilters {
  party?: ISelectOption;
  searchPhrase?: string;
}

const PoliticiansList: React.FC = () => {
  const dispatch = useDispatch();

  const [pageNumber, setPageNumber] = useState(1);
  const [filters, setFilters] = useState<IFilters>();
  const [partyOptions, setPartyOptions] = useState<ISelectOption[]>([]);

  const { politicians, loading } = useSelector(
    ({ politicians }: AppState) => politicians
  );

  useEffect(() => {
    const fetchParties = async () => {
      const res = await _fetch({ url: 'Parties' });

      if (res.data) {
        const newPartyOptions = res.data.map(({ id, longName }: IParty) => ({
          value: id,
          label: longName,
        }));
        const withoutPartyOption = { label: '— (nepartinis)', value: 'null' };

        setPartyOptions([withoutPartyOption, ...newPartyOptions]);
      }
    };

    fetchParties();
  }, []);

  useEffect(() => {
    if (filters) {
      dispatch(
        fetchPoliticians({
          PageNumber: 1,
          PageSize: POLITICIANS_FETCH_COUNT,
          PartyId: filters.party?.value,
          Search: filters.searchPhrase,
        })
      );
    }
  }, [filters]);

  useEffect(() => {
    dispatch(
      fetchPoliticians(
        {
          PageNumber: pageNumber,
          PageSize: POLITICIANS_FETCH_COUNT,
        },
        true
      )
    );
  }, [pageNumber]);

  return (
    <div className="flex flex-col items-center">
      <div className="flex flex-col w-full space-y-2 mb-4">
        <div className="flex-grow">
          <Input
            label="Paieška"
            placeholder="Paieška"
            className="w-full"
            onEnter={(value) => setFilters({ ...filters, searchPhrase: value })}
          />
        </div>
        <div className="flex-grow">
          <Select
            options={partyOptions}
            label="Partija"
            value={filters?.party}
            onChange={(value: any) => setFilters({ ...filters, party: value })}
            className="w-full"
            isClearable
          />
        </div>
      </div>
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
                          {party ? party.shortName : 'Nepartinis'}
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
