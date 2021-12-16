import { useState } from 'react';
import cx from 'classnames';
import Link from 'next/link';

interface IDropdownOption {
  label: string;
  href?: string;
  onClick?: (event: React.MouseEvent<HTMLElement>) => void | Promise<void>;
}

interface IDropdown {
  options?: IDropdownOption[];
}

const Dropdown: React.FC<IDropdown> = ({ children, options = [] }) => {
  const [clicked, setClicked] = useState(false);

  const toggleFocus = () => {
    setClicked(!clicked);
  };

  return (
    <div className="inline-block relative" onClick={toggleFocus}>
      {children}
      <ul
        className={cx('absolute', 'text-gray-700', 'pt-4', {
          hidden: !clicked,
          block: clicked,
        })}
      >
        {options.map(({ label, href, ...rest }) => {
          const classNames =
            'bg-gray-200 hover:bg-gray-400 py-2 px-4 block whitespace-no-wrap cursor-pointer';
          const key = `authorized-menu-item-${label}`;
          if (href) {
            return (
              <Link href={href} key={key}>
                <li>
                  <a className={classNames} {...rest}>
                    {label}
                  </a>
                </li>
              </Link>
            );
          }

          return (
            <li key={key}>
              <div className={classNames} {...rest}>
                {label}
              </div>
            </li>
          );
        })}
      </ul>
    </div>
  );
};

export default Dropdown;
