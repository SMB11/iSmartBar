import api from "../api";

export const ADDTOCART_START = "ADDTOCART_START";
export const ADDTOCART_END = "ADDTOCART_END";

const initialState = {
  cart: [],
  cartLoading: false
};

export default (state = initialState, action) => {
  switch (action.type) {
    case ADDTOCART_START:
      return { ...state, cartLoading: true };
    case ADDTOCART_END:
      let newCart = [...state.cart, action.payload];
      return { ...state, cartLoading: false, cart: newCart };
    default:
      return { ...state };
  }
};

export const addToCartStart = () => ({
  type: ADDTOCART_START
});
export const addToCartEnd = product => ({
  type: ADDTOCART_END,
  payload: product
});

export const addToCart = id => {
  return function(dispatch) {
    dispatch(addToCartStart());
    return api.locations
      .getAllCountries()
      .then(response => {
        dispatch(
          addToCartEnd({
            id: id,
            name: "xz",
            price: 12
          })
        );
        console.log("addtocart");
      })
      .catch(err => {
        console.log(err);
      });
  };
};
