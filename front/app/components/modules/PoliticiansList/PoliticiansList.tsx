import Link from 'next/link';

import { IPoliticians } from '@type/api/politicians';

const PoliticiansList: React.FC<IPoliticians> = ({ politicians }) => {
  return (
    <ul className="rounded shadow-lg divide-y">
      {politicians.map(({ id, fullName, party }) => {
        return (
          <li
            key={`politician-${id}`}
            className=" bg-white p-4 first:rounded-t last:rounded-b cursor-pointer"
          >
            <Link href="#">
              <a className="font-bold text-lg text-black hover:underline">
                {fullName}
              </a>
            </Link>
            <div className="flex justify-between font-semibold text-sm text-coolGray-500 mt-2">
              <Link href="#">
                <a className="font-normal hover:underline">{party}</a>
              </Link>

              {/* <div className="font-normal">
                Pareiškimų: <strong>{ @TODO: add statements count }</strong>
              </div> */}
            </div>
          </li>
        );
      })}
    </ul>
  );
};

export default PoliticiansList;
