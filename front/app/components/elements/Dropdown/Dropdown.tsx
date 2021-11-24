import { useState } from 'react';
import cx from 'classnames';
import Link from 'next/link';

interface IDropdownOption {
  label: string;
  href?: string;
  onClick?: () => void;
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
        className={cx('absolute', 'text-gray-700', 'pt-2', {
          hidden: !clicked,
          block: clicked,
        })}
      >
        {options.map(({ label, href = '', ...rest }) => {
          return (
            <Link href={href} key={`authorized-menu-item-${label}`}>
              <li>
                <a
                  className="bg-gray-200 hover:bg-gray-400 py-2 px-4 block whitespace-no-wrap cursor-pointer"
                  {...rest}
                >
                  {label}
                </a>
              </li>
            </Link>
          );
        })}
      </ul>
    </div>
  );
};

export default Dropdown;
