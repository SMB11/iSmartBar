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

export const setCart = cart => ({
  type: SET_CART,
  payload: cart
});

export const addToCart = (product, quantity) => {
  return dispatch => {
    let result = cartLogic.addToCart(product, quantity);
    if (result) {
      dispatch(setCart(result));
    }
  };
};
