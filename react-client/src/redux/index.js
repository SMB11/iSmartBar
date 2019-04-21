import { combineReducers } from "redux";
import language from "./language";
import locations from "./locations";
import cart from "./cart";
import category from "./category";

const rootReducer = combineReducers({
  language,
  locations,
  cart,
  category
});

export default rootReducer;
