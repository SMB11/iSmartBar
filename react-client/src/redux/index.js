import { combineReducers } from "redux";

import language from "./language";
import locations from "./locations";

const rootReducer = combineReducers({
  language,
  locations
});

export default rootReducer;
