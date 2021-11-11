import { AnyAction } from 'redux';

import {
  FETCH_STATEMENTS_REQUEST,
  FETCH_STATEMENTS_SUCCESS,
  FETCH_STATEMENTS_SUPPLEMENT_SUCCESS,
  FETCH_STATEMENTS_ERROR,
} from 'constants/actions';

const initialState = { loading: false, statements: { data: [] } };

const statementsReducer = (state = initialState, action: AnyAction) => {
  switch (action.type) {
    case FETCH_STATEMENTS_REQUEST:
      return { ...state, loading: true };
    case FETCH_STATEMENTS_SUCCESS:
      return { statements: action.payload, loading: false };
    case FETCH_STATEMENTS_SUPPLEMENT_SUCCESS:
      return {
        statements: {
          ...action.payload,
          data: [...state.statements.data, ...action.payload.data],
        },
        loading: false,
      };
    case FETCH_STATEMENTS_ERROR:
      return { statements: action.payload, loading: false };
    default:
      return state;
  }
};

export default statementsReducer;
