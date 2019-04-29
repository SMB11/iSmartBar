import api from "../api";

import {
    brandProductsSelector
} from "./selectors/product";


export const PRODUCTGET_START = "PRODUCTGET_START";
export const PRODUCTGET_END = "PRODUCTGET_END";


const initialState = {
    products: [],
    productsLoading: false
};


export default (state = initialState, action) => {
    switch (action.type) {
        case PRODUCTGET_START:
            return { ...state, productsLoading: true };
        case PRODUCTGET_END:
            const products = state.products.slice(0);
            products[action.payload.id] = action.payload.data;
            return { ...state, products };
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



export const GetBrandProducts = (lang, id) => {

    return (dispatch, getState) => {
        const state = getState();
        if (id && !brandProductsSelector(state, id)) {
            dispatch(ProductGetStart());
            return api.products
                .getProductsByBrandID(lang, id)
                .then(response => {
                    dispatch(ProductGetEnd(id, response.data));
                    console.log(response.data);
                })
                .catch(err => {

                });
        } else {
            //debugger;
        }
    };
};



