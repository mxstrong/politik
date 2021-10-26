import { BsChevronLeft } from 'react-icons/bs';
import Link from 'next/link';

interface IBackButtonProps {
  href: string;
}

const BackButton: React.FC<IBackButtonProps> = ({ href }) => {
  return (
    <Link href={href}>
      <a className="text-primary font-bold text-sm flex items-center cursor-pointer">
        <BsChevronLeft className="mr-1" /> Grįžti
      </a>
    </Link>
  );
};

export default BackButton;
