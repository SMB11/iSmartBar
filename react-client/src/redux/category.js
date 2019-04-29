import api from "../api";
import {
  rootCategorySelector,
  categoriesLoadingSelector,
  rootCategoriesBrandsLoadingSelector,
  rootCategoriesBrandsSelector
} from "./selectors/category";

export const CATEGORYGET_START = "CATEGORYGET_START";
export const CATEGORYGET_END = "CATEGORYGET_END";
export const GETROOTCATEGORYBRANDS_START = "GETROOTCATEGORYBRANDS_START";
export const GETROOTCATEGORYBRANDS_END = "GETROOTCATEGORYBRANDS_END";

const initialState = {
  categories: [],
  categoryBrands: [],
  categoryLoading: false,
  categoryBrandsLoading: false
};

export default (state = initialState, action) => {
  switch (action.type) {
    case CATEGORYGET_START:
      return { ...state, categoryLoading: true };
    case CATEGORYGET_END:
      return { ...state, categoryLoading: false, categories: action.payload };
    case GETROOTCATEGORYBRANDS_END:
      const categoryBrands = state.categoryBrands.slice(0);
      categoryBrands[action.payload.id] = action.payload.data;
      console.log(action);
      return { ...state, categoryBrands };
    default:
      return { ...state };
  }
};

export const CategoryGetStart = () => ({
  type: CATEGORYGET_START
});
export const CategoryGetEnd = categories => ({
  type: CATEGORYGET_END,
  payload: categories
});

export const GetRootCategoryBrandsStart = () => ({
  type: GETROOTCATEGORYBRANDS_START
});

export const GetRootCategoryBrandsEnd = (id, categoryBrands) => ({
  type: GETROOTCATEGORYBRANDS_END,
  payload: { id: id, data: categoryBrands }
});

export const CategoryGet = lang => {
  console.log(lang);

  return (dispatch, getState) => {
    const state = getState();
    if (
      !categoriesLoadingSelector(state) &&
      rootCategorySelector(state).length === 0
    ) {
      dispatch(CategoryGetStart());
      return api.categories
        .getAll(lang)
        .then(response => {
          dispatch(CategoryGetEnd(response.data));
        })
        .catch(err => {});
    } else {
      //debugger;
    }
  };
};

export const RootCategoryBrandGet = (lang, id) => {
  return (dispatch, getState) => {
    const state = getState();
    if (id && !rootCategoriesBrandsSelector(state, id)) {
      dispatch(GetRootCategoryBrandsStart());
      return api.brands
        .getSubcategoryBrands(lang, id)
        .then(response => {
          dispatch(GetRootCategoryBrandsEnd(id, response.data));
          console.log(response.data);
        })
        .catch(err => {});
    } else {
      //debugger;
    }
  };
};
