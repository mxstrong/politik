import { Dispatch } from 'redux';

import { getPaginationHeaderData, _fetch } from '@util/fetch';
import {
  FETCH_POLITICIANS_REQUEST,
  FETCH_POLITICIANS_SUCCESS,
  FETCH_POLITICIANS_SUPPLEMENT_SUCCESS,
  FETCH_POLITICIANS_ERROR,
} from 'constants/actions';

interface IFetchPoliticiansParams {
  pageNumber?: number;
  pageSize?: number;
}

export const fetchPoliticians =
  (params: IFetchPoliticiansParams, supplement: boolean = false) =>
  async (dispatch: Dispatch) => {
    dispatch({ type: FETCH_POLITICIANS_REQUEST });

    const res = await _fetch({ url: 'Politicians', params });

    if (res.data) {
      return dispatch({
        type: supplement
          ? FETCH_POLITICIANS_SUPPLEMENT_SUCCESS
          : FETCH_POLITICIANS_SUCCESS,
        payload: { ...getPaginationHeaderData(res.headers), data: res.data },
      });
    }

    return dispatch({ type: FETCH_POLITICIANS_ERROR, payload: res.error });
  };
