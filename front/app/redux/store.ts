import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';

import rootReducer from './reducers';

const enhancers: [] = [];
const initialState = {};

const middleware = [thunk];

const composedEnhancers = compose(applyMiddleware(...middleware), ...enhancers);

export default function configureStore() {
  return createStore(rootReducer, initialState, composedEnhancers);
}
