// import { createSelector } from "reselect";

export const rootCategorySelector = state =>
  state.category.categories.filter(category => category.parentID == null);
export const subCategorySelector = (state, id) =>
  state.category.categories.filter(category => category.parentID === id);
export const categorySelector = (state, id) =>
  state.category.categories.find(c => c.id === id);
export const categoriesLoadingSelector = (state, id) =>
  state.category.categoryLoading;

export const rootCategoriesBrandsLoadingSelector = state =>
  state.category.categoryBrandsLoading;

export const rootCategoriesBrandsSelector = (state, id) =>
  state.category.categoryBrands[id];
