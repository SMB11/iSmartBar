import { combineReducers } from "redux";
import language from "./language";
import locations from "./locations";
import cart from "./cart";

const rootReducer = combineReducers({
  language,
  locations,
  cart
});

export default rootReducer;
