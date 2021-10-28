import { Dispatch } from 'redux';

import { _fetch } from '@api/RestClient';
import {
  FETCH_POLITICIANS_REQUEST,
  FETCH_POLITICIANS_SUCCESS,
  FETCH_POLITICIANS_ERROR,
} from 'constants/actions';

export const fetchPoliticians = () => async (dispatch: Dispatch) => {
  dispatch({ type: FETCH_POLITICIANS_REQUEST });

  const res = await _fetch({ url: 'Politicians' });

  if (res.data) {
    return dispatch({ type: FETCH_POLITICIANS_SUCCESS, payload: res.data });
  }

  return dispatch({ type: FETCH_POLITICIANS_ERROR, payload: res.error });
};
