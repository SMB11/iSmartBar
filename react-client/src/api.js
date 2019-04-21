import axios from "axios";
import { withLocalize } from "react-localize-redux";
const apiUrl = "https://localhost:44396/api/";
const iSmartBarApiUrl = "https://localhost:44316/api/";

export default {
  locations: {
    getAllCountries: () => axios.get(`${iSmartBarApiUrl}locations/countries`),
    getAllCities: id => axios.get(`${iSmartBarApiUrl}locations/cities/${id}`),
    getAllHotels: id => axios.get(`${iSmartBarApiUrl}locations/hotels/${id}`)
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
