import Link from 'next/link';
import { useRouter } from 'next/router';

import { IPoliticians } from '@type/api/politicians';

const PoliticiansList: React.FC<IPoliticians> = ({ politicians }) => {
  const router = useRouter();

  return (
    <ul className="rounded shadow-lg divide-y">
      {politicians.map(({ id, fullName, party }) => {
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
