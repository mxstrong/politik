import { useState } from 'react';
import Link from 'next/link';
import { useRouter } from 'next/router';
import { BsList, BsX } from 'react-icons/bs';

import PrimaryMenuItem from './PrimaryMenuItem';
import IconButton from '@element/IconButton';

export const HEADER_PRIMARY_MENU_ITEMS = [
  { path: '/', label: 'Naujausi pareiškimai' },
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
    <header className="shadow-lg bg-white">
      <div className="max-w-3xl mx-auto px-4 md:px-6">
        <div className="flex justify-between">
          <div className="flex space-x-7">
            <div>
              <Link href="/">
                <a className="flex items-center py-4 px-2">
                  <span className="font-semibold text-secondary text-lg">
                    {'<LOGO>'}
                  </span>
                </a>
              </Link>
            </div>
            <nav className="hidden md:flex items-center space-x-1">
              {HEADER_PRIMARY_MENU_ITEMS.map(({ path, label }) => {
                return (
                  <PrimaryMenuItem
                    key={`header-primary-menu-item-${path}`}
                    path={path}
                    label={label}
                    active={isItemActive(path)}
                  />
                );
              })}
            </nav>
          </div>
          <div className="md:hidden flex items-center">
            <IconButton onClick={toggleMenu}>
              {mobileMenuOpen ? (
                <BsX className="w-6 h-6" />
              ) : (
                <BsList className="w-6 h-6" />
              )}
            </IconButton>
          </div>
        </div>
      </div>

      {mobileMenuOpen && (
        <div className="md:hidden absolute w-screen bg-white shadow-lg">
          <nav>
            {HEADER_PRIMARY_MENU_ITEMS.map(({ path, label }) => {
              return (
                <PrimaryMenuItem
                  key={`header-primary-menu-item-${path}`}
                  path={path}
                  label={label}
                  active={isItemActive(path)}
                  mobile
                  onClick={toggleMenu}
                />
              );
            })}
          </nav>
        </div>
      )}
    </header>
  );
};

export default Header;
