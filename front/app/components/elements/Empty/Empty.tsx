import { BsX } from 'react-icons/bs';

const Empty = () => {
  return (
    <div className="flex flex-col items-center text-gray-600">
      <BsX className="w-16 h-16" />
      <div className="mt-3 font-semibold">Įrašų kol kas nėra</div>
    </div>
  );
};

export default Empty;
