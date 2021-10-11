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
    ? cx('block', 'text-xl', 'p-5', 'font-medium', 'hover:text-primary', {
        'text-primary': active,
        'font-extrabold': active,
        'text-primary-dark': !active,
      })
    : cx('py-4', 'px-2', 'font-semibold', 'hover:text-primary', {
        'text-primary': active,
        'border-b-4': active,
        'border-primary': active,
        'text-primary-dark': !active,
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
