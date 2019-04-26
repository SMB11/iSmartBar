// import { createSelector } from "reselect";

export const currentCartSelector = state =>
  state.cart.carts[state.cart.carts.length];
