import axios from "axios";
const apiUrl = "https://localhost:44396/api/";

export default {
  locations: {
    getAllCountries: () => axios.get(`${apiUrl}locations/countries`),
    getAllCities: id => axios.get(`${apiUrl}locations/cities/${id}`),
    getAllHotels: id => axios.get(`${apiUrl}locations/hotels/${id}`)
  }
};
