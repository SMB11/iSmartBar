import axios from "axios";
import { withLocalize } from "react-localize-redux";
const apiUrl = "https://localhost:44396/api/";
const iSmartBarApiUrl = "https://localhost:44316/api/";

const getHeader = lang => {
  if (lang)
    return {
      headers: { "Accept-Language": lang }
    };
  else return {};
};
export const assetBaseUrl = "https://localhost:44396/"

export default {
  locations: {
    getAllCountries: lang =>
      axios.get(`${iSmartBarApiUrl}locations/countries`, getHeader(lang)),
    getAllCities: (id, lang) =>
      axios.get(`${iSmartBarApiUrl}locations/cities/${id}`, getHeader(lang)),
    getAllHotels: id => axios.get(`${iSmartBarApiUrl}locations/hotels/${id}`)
  },
  categories: {
    getAll: lang => {
      return axios.get(`${apiUrl}category`, getHeader(lang));
    }
  },
  brands: {
    getSubcategoryBrands: (lang, id) => {
      return axios.get(`${apiUrl}brand/subcategoriesBrands/${id}`, getHeader(lang))
    }
  },
  products: {
    getProductsByBrandID: (lang, id) => {
      return axios.get(`${apiUrl}product/brand/${id}`, getHeader(lang))

    }
  }
};
