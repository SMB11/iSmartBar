import axios from "axios";
import { withLocalize } from "react-localize-redux";
import Cookies from "universal-cookie";
const apiUrl =
"https://localhost:44396/api/";
  // "https://mybar.azurewebsites.net/api/";

const getHeader = lang => {
  
  let currency = localStorage.getItem("currency");
  if(!currency) currency = "USD";
  let headers = {currency};
  if (lang)
    headers["Accept-Language"] = lang;
  return {headers};
};

const getTimestamep = () => new Date().getTime();

const getCoreUrl = (path, cache) => {
  return apiUrl + path + (cache ? `?t=${getTimestamep()}` : "");
};

export default {
  locations: {
    getAllCountries: lang =>
      axios.get(`${apiUrl}locations/countries`, getHeader(lang)),
    getAllCities: (id, lang) =>
      axios.get(`${apiUrl}locations/cities/${id}`, getHeader(lang)),
    getAllHotels: id => axios.get(`${apiUrl}locations/hotels/${id}`)
  },
  visit: {
    add: visit => axios.post(`${apiUrl}visit/add`, visit)
  },
  categories: {
    getAll: lang => {
      return axios.get(getCoreUrl("category", true), getHeader(lang));
    }
  },
  brands: {
    getSubcategoryBrands: (lang, id) => {
      return axios.get(
        getCoreUrl(`brand/subcategoriesBrands/${id}`, true),
        getHeader(lang)
      );
    }
  },
  cart:{
    validate: (carts) => {
      return axios.post(
        getCoreUrl(`cart/validate`),
        carts,
        getHeader()
      );
    }
  },
  products: {
    getProductsByBrandAndCategory: (lang, brandID, categoryID) => {
      return axios.get(
        getCoreUrl(`product/category/${categoryID}/brand/${brandID}`, true),
        getHeader(lang)
      );
    },
    getProductDescription: (lang, id) => {
      return axios.get(getCoreUrl(`product/${id}`, true), getHeader(lang));
    },
    getTopFive: lang => {
      return axios.get(getCoreUrl(`product/topFive`, true), getHeader(lang));
    }
  }
};
