// import { createSelector } from "reselect";

export const rootCategorySelector = state =>
  state.category.categories.filter(category => category.parentID == null);
export const subCategorySelector = (state, id) =>
  state.category.categories.filter(category => category.parentID === id);

export const categoriesLoadingSelector = (state, id) =>
  state.category.categoryLoading;
