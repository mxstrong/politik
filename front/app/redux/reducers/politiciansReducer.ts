import { AnyAction } from 'redux';

import {
  FETCH_POLITICIANS_REQUEST,
  FETCH_POLITICIANS_SUCCESS,
  FETCH_POLITICIANS_ERROR,
} from 'constants/actions';

const initialState = { loading: false, politicians: [] };

const politiciansReducer = (state = initialState, action: AnyAction) => {
  switch (action.type) {
    case FETCH_POLITICIANS_REQUEST:
      return { ...state, loading: true };
    case FETCH_POLITICIANS_SUCCESS:
      return { politicians: action.payload, loading: false };
    case FETCH_POLITICIANS_ERROR:
      return { politicians: action.payload, loading: false };
    default:
      return state;
  }
};

export default politiciansReducer;
