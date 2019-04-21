import axios from "axios";
import { withLocalize } from "react-localize-redux";
const apiUrl = "https://localhost:44396/api/";

export default {
  locations: {
    getAllCountries: () => axios.get(`${apiUrl}locations/countries`),
    getAllCities: id => axios.get(`${apiUrl}locations/cities/${id}`),
    getAllHotels: id => axios.get(`${apiUrl}locations/hotels/${id}`)
  },
  categories: {
    getAll: lang => {
      let headers = {};
      if (lang)
        headers = {
          headers: { "Accept-Language": lang }
        };
      return axios.get(`${apiUrl}category`, headers);
    }
  }
};
