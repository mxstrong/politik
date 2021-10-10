import { useState } from 'react';
import Link from 'next/link';
import { useRouter } from 'next/router';
import { BsList, BsX } from 'react-icons/bs';
import cx from 'classnames';

export const HEADER_PRIMARY_MENU_ITEMS = [
  { path: '/', label: 'Naujausi pranešimai' },
  {
    path: '/politicians',
    label: 'Politikai',
  },
];

const Header = () => {
  const router = useRouter();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const toggleMenu = () => setMobileMenuOpen(!mobileMenuOpen);

  const isItemActive = (path: string) => path === router.pathname;

  return (
    <header className="bg-white shadow-lg">
      <div className="max-w-6xl mx-auto px-4">
        <div className="flex justify-between">
          <div className="flex space-x-7">
            <div>
              <Link href="/">
                <a className="flex items-center py-4 px-2">
                  <span className="font-semibold text-gray-500 text-lg">
                    {'<LOGO>'}
                  </span>
                </a>
              </Link>
            </div>
            <nav className="hidden md:flex items-center space-x-1">
              {HEADER_PRIMARY_MENU_ITEMS.map(({ path, label }) => {
                const active = isItemActive(path);
                const linkClassNames = cx('py-4', 'px-2', 'font-semibold', {
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
                    <a className={linkClassNames}>{label}</a>
                  </Link>
                );
              })}
            </nav>
          </div>
          <div className="md:hidden flex items-center">
            <button
              className="outline-none mobile-menu-button flex items-center"
              onClick={toggleMenu}
            >
              {mobileMenuOpen ? (
                <BsX className="w-6 h-6" />
              ) : (
                <BsList className="w-6 h-6" />
              )}
            </button>
          </div>
        </div>
      </div>

      {mobileMenuOpen && (
        <div className="md:hidden">
          <nav>
            {HEADER_PRIMARY_MENU_ITEMS.map(({ path, label }) => {
              const active = isItemActive(path);
              const linkClassNames = cx('block', 'text-sm', 'px-2', 'py-4', {
                'text-white': active,
                'bg-indigo-800': active,
                'font-semibold': active,
                'hover:bg-indigo-800': !active,
                transition: !active,
                'duration-300': !active,
              });

              return (
                <Link key={`menu-item-${path}`} href={path}>
                  <a className={linkClassNames} onClick={toggleMenu}>
                    {label}
                  </a>
                </Link>
              );
            })}
          </nav>
        </div>
      )}
    </header>
  );
};

export default Header;
