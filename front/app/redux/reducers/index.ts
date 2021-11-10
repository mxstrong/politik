import { combineReducers, Reducer } from 'redux';
import politicians from './politiciansReducer';
import statements from './statementsReducer';

const rootReducer: Reducer = combineReducers({
  politicians,
  statements,
});

export default rootReducer;

export type AppState = ReturnType<typeof rootReducer>;
