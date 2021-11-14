import { Dispatch } from 'redux';

import { getPaginationHeaderData, _fetch } from '@util/fetch';
import {
  FETCH_STATEMENTS_REQUEST,
  FETCH_STATEMENTS_SUCCESS,
  FETCH_STATEMENTS_SUPPLEMENT_SUCCESS,
  FETCH_STATEMENTS_ERROR,
  FETCH_STATEMENTS_CLEAR,
} from 'constants/actions';

interface IFetchStatementsParams {
  pageNumber?: number;
  pageSize?: number;
  politician?: string;
  tags?: string;
}

export const fetchStatements =
  (params: IFetchStatementsParams, supplement: boolean = false) =>
  async (dispatch: Dispatch) => {
    dispatch({ type: FETCH_STATEMENTS_REQUEST });

    const res = await _fetch({ url: 'Statements', params });

    if (res.data) {
      return dispatch({
        type: supplement
          ? FETCH_STATEMENTS_SUPPLEMENT_SUCCESS
          : FETCH_STATEMENTS_SUCCESS,
        payload: { ...getPaginationHeaderData(res.headers), data: res.data },
      });
    }

    return dispatch({ type: FETCH_STATEMENTS_ERROR, payload: res.error });
  };
