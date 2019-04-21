// import { createSelector } from "reselect";

export const countriesSelector = state => state.locations.countries;
export const countriesFetchingSelector = state =>
  state.locations.countriesFetching;

export const citiesSelector = state => state.locations.cities;
export const citiesFetchingSelector = state => state.locations.citiesFetching;

export const hotelsSelector = state => state.locations.hotels;
export const hotelsFetchingSelector = state => state.locations.hotelsFetching;
