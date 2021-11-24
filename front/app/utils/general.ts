import { MOD_ROLES } from 'constants/userTypes';
import { parseLocalStorageItem } from './storage';

export const isClientSide = typeof window !== 'undefined';

export const isMod = (): boolean => {
  return MOD_ROLES.includes(parseLocalStorageItem('currentUser').role);
};
