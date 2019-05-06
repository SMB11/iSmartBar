import { combineReducers } from "redux";
import language from "./language";
import locations from "./locations";
import cart from "./cart";
import category from "./category";
import popup from "./popup";
import products from "./products";

const rootReducer = combineReducers({
  language,
  locations,
  cart,
  category,
  products,
  popup
});

export default rootReducer;
