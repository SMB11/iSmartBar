// import { createSelector } from "reselect";

export const currentCartSelector = state =>
  state.cart.carts[state.cart.carts.length - 1];

export const sectionCartSelector = (state, size) => currentCartSelector(state)[size - 1];
export const insidePriceSelector = (state) => {
  const cart = currentCartSelector(state);
  const reducer = (price, current) => price + current.price * current.quantity;
  let price = 0;
  for (let i = 0; i < 5; i++) {
    price += cart[i].reduce(reducer, 0);
  }
  return price.toFixed(2);
}
export const outisdePriceSelector = (state) => {
  const cart = currentCartSelector(state);
  const reducer = (price, current) => price + current.price * current.quantity;
  let price = 0;
  price += cart[5].reduce(reducer, 0);

  return price.toFixed(2);
}
export const cartFullCountSelector = (state) => {
  const cart = currentCartSelector(state);
  const reducer = (count, current) => count + current.quantity;
  let quantity = 0;
  for (let i = 0; i < 6; i++) {
    quantity += cart[i].reduce(reducer, 0);
  }
  return quantity;
}

