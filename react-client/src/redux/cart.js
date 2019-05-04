import api from "../api";
import cartLogic from "../logic/cart";

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

export const addToCart = (product, quantity) => {
  return dispatch => {
    let result = cartLogic.addToCart(product, quantity);
    if (result.carts) {
      dispatch(setCart(result.carts));
    }
  };
};

export const changeProductQuantity = (id, size, quantity) => {
  return dispatch => {
    let result = cartLogic.changeProductQuantity(id, size, quantity);
    if (result.carts) {
      dispatch(setCart(result.carts));
    }
  };
}
export const removeProduct = (id, size) => {
  return dispatch => {
    let result = cartLogic.removeProduct(id, size);
    if (result.carts) {
      dispatch(setCart(result.carts));
    }
    if (result.popupMessage) {
      alert(result.popupMessage);
    }
  };
}