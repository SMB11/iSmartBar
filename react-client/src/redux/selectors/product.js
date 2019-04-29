export const productsLoadingSelector = (state) =>
    state.products.productsLoading;


export const brandProductsSelector = (state, id) =>
    state.products.products[id];

export const productDescriptionSelector = (state) =>
    state.products.productDescription;
