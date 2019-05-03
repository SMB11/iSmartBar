import api from "../api";

import {
  brandProductsSelector,
  topFiveSelector,
  topFiveLoadingSelector
} from "./selectors/product";

export const PRODUCTGET_START = "PRODUCTGET_START";
export const PRODUCTGET_END = "PRODUCTGET_END";
export const PRODUCTDESCTIPTIONGET_START = "PRODUCTDESCTIPTIONGET_START";
export const PRODUCTDESCTIPTIONGET_END = "PRODUCTDESCTIPTIONGET_END";
export const TOPFIVEGET_START = "TOPFIVEGET_START";
export const TOPFIVEGET_END = "TOPFIVEGET_END";

const initialState = {
  products: [],
  productDescription: {},
  productsLoading: false,
  productDescriptionLoading: false,
  topFive: [],
  topFiveLoading: false
};

export default (state = initialState, action) => {
  switch (action.type) {
    case PRODUCTGET_START:
      return { ...state, productsLoading: true };
    case PRODUCTGET_END:
      const products = state.products.slice(0);
      products[action.payload.id] = action.payload.data;
      return { ...state, products, productsLoading: false };

    case PRODUCTDESCTIPTIONGET_START:
      return {
        ...state,
        productDescription: {},
        productDescriptionLoading: true
      };
    case PRODUCTDESCTIPTIONGET_END:
      return {
        ...state,
        productDescription: action.payload,
        productDescriptionLoading: false
      };

    case TOPFIVEGET_START:
      return { ...state, topFiveLoading: true };
    case TOPFIVEGET_END:
      return { ...state, topFive: action.payload, topFiveLoading: false };

    default:
      return { ...state };
  }
};

export const ProductGetStart = () => ({
  type: PRODUCTGET_START
});
export const ProductGetEnd = (id, products) => ({
  type: PRODUCTGET_END,
  payload: { id: id, data: products }
});

export const TopFiveGetStart = () => ({
  type: TOPFIVEGET_START
});
export const TopFiveGetEnd = products => ({
  type: TOPFIVEGET_END,
  payload: products
});
export const ProductDescriptionGetStart = () => ({
  type: PRODUCTDESCTIPTIONGET_START
});
export const ProductDescriptionGetEnd = product => ({
  type: PRODUCTDESCTIPTIONGET_END,
  payload: product
});

export const GetBrandProducts = (lang, id) => {
  return (dispatch, getState) => {
    const state = getState();
    if (id && !brandProductsSelector(state, id)) {
      dispatch(ProductGetStart());
      return api.products
        .getProductsByBrandID(lang, id)
        .then(response => {
          dispatch(ProductGetEnd(id, response.data));
        })
        .catch(err => {});
    } else {
      //debugger;
    }
  };
};

export const GetProductDescription = (lang, id) => {
  return (dispatch, getState) => {
    const state = getState();
    dispatch(ProductDescriptionGetStart());
    return api.products
      .getProductDescription(lang, id)
      .then(response => {
        dispatch(ProductDescriptionGetEnd(response.data));
        console.log(response.data);
      })
      .catch(err => {
        console.log(err);
      });
  };
};
export const GetTopFive = lang => {
  return (dispatch, getState) => {
    const state = getState();
    if (!topFiveLoadingSelector(state) && !topFiveSelector(state).length) {
      dispatch(TopFiveGetStart());
      return api.products
        .getTopFive(lang)
        .then(response => {
          dispatch(TopFiveGetEnd(response.data));
        })
        .catch(err => {});
    } else {
      //debugger;
    }
  };
};
