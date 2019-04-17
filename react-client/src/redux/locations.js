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

function fetchSecretSauce() {
  return fetch("http://localhost:3000/");
}

export function GetCountries() {
  return function(dispatch) {
    dispatch(fetchCountriesStart());
    return fetchSecretSauce().then(data =>
      dispatch(
        fetchCountriesEnd([
          { id: 1, name: "America" },
          { id: 2, name: "Italy" }
        ])
      )
    );
  };
}

export function GetCities(countryID) {
  return function(dispatch) {
    dispatch(fetchCitiesStart());
    return fetchSecretSauce().then(data => {
      let cities = [];
      if (countryID === 1) {
        cities = [{ id: 1, name: "New York" }, { id: 2, name: "Chicago" }];
      } else {
        cities = [{ id: 1, name: "Rome" }, { id: 2, name: "Florence" }];
      }
      dispatch(fetchCitiesEnd(cities));
    });
  };
}

export function GetHotels(cityID) {
  return function(dispatch) {
    dispatch(fetchHotelsStart());
    return fetchSecretSauce().then(data => {
      let hotels = [];
      if (cityID === 1) {
        hotels = [
          { id: 1, name: "Best Western" },
          { id: 2, name: "Palm Springs" }
        ];
      } else {
        hotels = [
          { id: 1, name: "Raddison Blu" },
          { id: 2, name: "Al Theatro Palace" }
        ];
      }
      dispatch(fetchHotelsEnd(hotels));
    });
  };
}
