import { createSelector } from "reselect";

export const languagesSelector = state => state.language.languages;
export const languagesFetchingSelector = state =>
  state.language.languagesFetching;
