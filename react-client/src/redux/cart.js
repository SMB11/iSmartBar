import api from "../api";
import cartLogic from "../logic/cart";
import { SetPopupMessage } from "./popup";

export const SET_CART = "SET_CART";
const initialState = {
  carts: cartLogic.getCarts(),
  cartLoading: false
};

export default (state = initialState, action) => {
  switch (action.type) {
    case SET_CART:
      return { ...state, carts: action.payload };

    default:
      return { ...state };
  }
};

const setCart = cart => ({
  type: SET_CART,
  payload: cart
});
const processResult = (dispatch, result) => {
  if (result.carts) {
    dispatch(setCart(result.carts));
  }
  if (result.popupMessage) {
    dispatch(SetPopupMessage(result.popupMessage));
  }
};

export const addToCart = (product, quantity) => {
  return dispatch => {
    let result = cartLogic.addToCart(product, quantity);

    processResult(dispatch, result);
  };
};

export const changeProductQuantity = (id, size, quantity) => {
  return dispatch => {
    let result = cartLogic.changeProductQuantity(id, size, quantity);

    processResult(dispatch, result);
  };
};
export const removeProduct = (id, size) => {
  return dispatch => {
    let result = cartLogic.removeProduct(id, size);
    processResult(dispatch, result);
  };
};
