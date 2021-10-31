import { combineReducers, Reducer } from 'redux';
import politicians from './politiciansReducer';

const rootReducer: Reducer = combineReducers({
  politicians,
});

export default rootReducer;

export type AppState = ReturnType<typeof rootReducer>;
