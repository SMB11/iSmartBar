import { combineReducers } from "redux";
import language from "./language";
import locations from "./locations";
import cart from "./cart";
import category from "./category";
import products from "./products";

const rootReducer = combineReducers({
  language,
  locations,
  cart,
  category,
  products
});

export default rootReducer;
