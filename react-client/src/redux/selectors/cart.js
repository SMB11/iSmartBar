// import { createSelector } from "reselect";

export const currentCartSelector = state =>
  state.cart.carts[state.cart.carts.length - 1];

const countReducer = (count, current) => count + current.quantity;
export const sectionCartSelector = (state, size) =>
  currentCartSelector(state)[size - 1];
export const cartAllSelector = state => {
  const cart = currentCartSelector(state);
  const res = [];
  for (let i = 0; i < 2; i++) {
    Array.prototype.push.apply(res, cart[i]);
  }
  console.log(res);
  return res;
};
export const sectionCartCountSelector = (state, size) =>
  currentCartSelector(state)[size - 1].reduce(countReducer, 0);
export const insidePriceSelector = state => {
  const cart = currentCartSelector(state);
  const reducer = (price, current) => price + current.price * current.quantity;
  let price = 0;
  for (let i = 0; i < 2; i++) {
    price += cart[i].reduce(reducer, 0);
  }
  return price.toFixed(2);
};
export const outisdePriceSelector = state => {
  const cart = currentCartSelector(state);
  const reducer = (price, current) => price + current.price * current.quantity;
  let price = 0;
  price += cart[2].reduce(reducer, 0);

  return price.toFixed(2);
};
export const cartFullCountSelector = state => {
  const cart = currentCartSelector(state);
  let quantity = 0;
  for (let i = 0; i < 3; i++) {
    quantity += cart[i].reduce(countReducer, 0);
  }
  return quantity;
};
