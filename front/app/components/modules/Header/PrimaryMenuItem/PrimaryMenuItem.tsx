import Link from 'next/link';
import cx from 'classnames';

interface IPrimaryMenuItem extends React.HTMLAttributes<HTMLAnchorElement> {
  path: string;
  label: string;
  active?: boolean;
  mobile?: boolean;
}

const PrimaryMenuItem: React.FC<IPrimaryMenuItem> = ({
  path,
  label,
  active = false,
  mobile = false,
  ...rest
}) => {
  const classNames = mobile
    ? cx('block', 'text-sm', 'px-2', 'py-4', {
        'text-white': active,
        'bg-indigo-800': active,
        'font-semibold': active,
        'hover:bg-indigo-800': !active,
        transition: !active,
        'duration-300': !active,
      })
    : cx('py-4', 'px-2', 'font-semibold', {
        'text-indigo-800': active,
        'border-b-4': active,
        'border-indigo-800': active,
        transition: active,
        'duration-300': active,
        'text-gray-500': !active,
        'hover:text-indigo-800': !active,
      });

  return (
    <Link key={`menu-item-${path}`} href={path}>
      <a className={classNames} {...rest}>
        {label}
      </a>
    </Link>
  );
};

export default PrimaryMenuItem;
