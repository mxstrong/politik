import { isClientSide } from './general';

export const getLocalStorageItem = (key: string): string | null | undefined => {
  if (isClientSide) {
    return localStorage.getItem(key);
  }

  return undefined;
};

export const parseLocalStorageItem = (key: string) => {
  try {
    return JSON.parse(getLocalStorageItem(key) || '');
  } catch (e) {
    return null;
  }
};

export const setLocalStorageItem = (key: string, value: string) => {
  if (isClientSide) {
    localStorage.setItem(key, value);
  }
};

export const removeLocalStorageItem = (key: string) => {
  if (isClientSide) {
    localStorage.removeItem(key);
  }
};
