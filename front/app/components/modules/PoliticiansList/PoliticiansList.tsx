import Link from 'next/link';

const POLITICIANS = [
  { id: 1, name: 'Mantas Ptakauskas', party: 'LSDP', statementsCount: 4 },
  { id: 2, name: 'Paulius Zaranka', party: 'TS-LKD', statementsCount: 3 },
  { id: 3, name: 'Ingrida Šimonytė', party: 'TS-LKD', statementsCount: 1000 },
];

const PoliticiansList = () => {
  return (
    <ul className="rounded shadow-lg divide-y">
      {POLITICIANS.map(({ id, name, party, statementsCount }) => {
        return (
          <li
            key={`politician-${id}`}
            className=" bg-white p-4 first:rounded-t last:rounded-b cursor-pointer"
          >
            <Link href="#">
              <a className="font-bold text-lg text-black hover:underline">
                {name}
              </a>
            </Link>
            <div className="flex justify-between font-semibold text-sm text-coolGray-500 mt-2">
              <Link href="#">
                <a className="font-normal hover:underline">{party}</a>
              </Link>
              <div className="font-normal">
                Pareiškimų: <strong>{statementsCount}</strong>
              </div>
            </div>
          </li>
        );
      })}
    </ul>
  );
};

export default PoliticiansList;
