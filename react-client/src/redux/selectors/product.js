export const brandProductsLoadingSelector = state =>
  state.products.productsLoading;

export const brandProductsSelector = (state, id) => state.products.products[id];

export const productDescriptionSelector = state =>
  state.products.productDescription;
export const productDescriptionLaodingSelector = state =>
  state.products.productDescriptionLoading;

export const topFiveSelector = state => state.products.topFive;
export const topFiveLoadingSelector = state => state.products.topFiveLoading;
