import api from "../api";

export const FETCH_COUNTRIES = "language/FETCH_COUNTRIES";
export const FETCH_COUNTRIES_END = "language/FETCH_COUNTRIES_END";
export const FETCH_CITIES = "language/FETCH_CITIES";
export const FETCH_CITIES_END = "language/FETCH_CITIES_END";
export const FETCH_HOTELS = "language/FETCH_HOTELS";
export const FETCH_HOTELS_END = "language/FETCH_HOTELS_END";
// export const CART_UPDATED = 'cart/CART_UPDATED';

const initialState = {
  countries: [],
  countriesFetching: false,
  cities: [],
  citiesFetching: false,
  hotels: [],
  hotelsFetching: false
};

export default (state = initialState, action) => {
  switch (action.type) {
    case FETCH_COUNTRIES:
      return { ...state, countriesFetching: true };
    case FETCH_COUNTRIES_END:
      return { ...state, countriesFetching: false, countries: action.payload };
    case FETCH_CITIES:
      return { ...state, citiesFetching: true };
    case FETCH_CITIES_END:
      return { ...state, citiesFetching: false, cities: action.payload };
    case FETCH_HOTELS:
      return { ...state, hotelsFetching: true };
    case FETCH_HOTELS_END:
      return { ...state, hotelsFetching: false, hotels: action.payload };
    default:
      return { ...state };
  }
};

export const fetchCountriesStart = () => ({
  type: FETCH_COUNTRIES
});

export const fetchCountriesEnd = countries => ({
  type: FETCH_COUNTRIES_END,
  payload: countries
});

export const fetchCitiesStart = () => ({
  type: FETCH_CITIES
});

export const fetchCitiesEnd = cities => ({
  type: FETCH_CITIES_END,
  payload: cities
});

export const fetchHotelsStart = () => ({
  type: FETCH_HOTELS
});

export const fetchHotelsEnd = cities => ({
  type: FETCH_HOTELS_END,
  payload: cities
});

export function GetCountries() {
  return function(dispatch) {
    dispatch(fetchCountriesStart());
    return api.locations
      .getAllCountries()
      .then(response => {
        dispatch(fetchCountriesEnd(response.data));
      })
      .catch(err => {
        console.log(err);
      });
  };
}

export function GetCities(countryID) {
  return function(dispatch) {
    dispatch(fetchCitiesStart());
    return api.locations
      .getAllCities(countryID)
      .then(response => {
        dispatch(fetchCitiesEnd(response.data));
      })
      .catch(err => {
        console.log(err);
      });
  };
}

export function GetHotels(cityID) {
  return function(dispatch) {
    dispatch(fetchHotelsStart());
    return api.locations
      .getAllHotels(cityID)
      .then(response => {
        dispatch(fetchHotelsEnd(response.data));
      })
      .catch(err => {
        console.log(err);
      });
  };
}
