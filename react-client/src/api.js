import axios from "axios";
import { withLocalize } from "react-localize-redux";
const apiUrl =
  //"https://localhost:44396/api/";
  "http://coreapi-env.txt38iawzw.eu-west-3.elasticbeanstalk.com/api/";
const iSmartBarApiUrl =
  "http://ismartbarapi-env.txt38iawzw.eu-west-3.elasticbeanstalk.com/api/";

const getHeader = lang => {
  if (lang)
    return {
      headers: { "Accept-Language": lang }
    };
  else return {};
};

const getTimestamep = () => new Date().getTime();

const getCoreUrl = (path, cache) => {
  return apiUrl + path + (cache ? `?t=${getTimestamep()}` : "");
};

export const assetBaseUrl =
  "http://coreapi-env.txt38iawzw.eu-west-3.elasticbeanstalk.com/";

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
