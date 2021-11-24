import { _fetch } from './fetch';
import { isClientSide, setLocalStorageItem } from './storage';

export const fetchUser = async () => {
  if (isClientSide) {
    const res = await _fetch({ url: 'Auth/CurrentUser' });

    if (res.data) {
      setLocalStorageItem('currentUser', JSON.stringify(res.data));
    }

    return res.data;
  }

  return;
};
