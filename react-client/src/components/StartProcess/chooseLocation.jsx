import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { GetCountries, GetCities, GetHotels } from "../../redux/locations";
import {
  withLocalize,
  Translate,
  setActiveLanguage
} from "react-localize-redux";
import DropDown from "../Reusable/dropdown";
import {
  countriesSelector,
  countriesFetchingSelector,
  citiesSelector,
  citiesFetchingSelector,
  hotelsFetchingSelector,
  hotelsSelector
} from "../../redux/selectors/locations";

export const locationStepStorageKey = "ChooseLocationState";

class ChooseLocation extends Component {
  state = {
    country: null,
    city: null,
    hotel: null
  };
  componentDidMount() {
    this.props.GetCountries();
    const state = JSON.parse(
      window.sessionStorage.getItem(locationStepStorageKey)
    );
    console.log(state);
    if (state) {
      this.props.GetCountries();
      this.props.GetCities(state.country.id);
      this.props.GetHotels(state.city.id);
      this.setState(state);
    }
  }
  componentDidUpdate(prevProps, prevState) {}

  handleCountryChanged(e) {
    const country = this.props.countries.find(
      cnt => cnt.id === parseInt(e.target.value)
    );
    if (country) {
      this.setState({ ...this.state, country: country });
      this.props.GetCities(country.id);
    } else {
      this.setState({ ...this.state, country: null });
    }

    this.setState({ ...this.state, city: null, hotel: null });
  }

  handleCityChanged(e) {
    const city = this.props.cities.find(
      cnt => cnt.id === parseInt(e.target.value)
    );
    if (city) {
      this.setState({ ...this.state, city: city });
      this.props.GetHotels(city.id);
    } else {
      this.setState({ ...this.state, city: null });
    }
    this.setState({ ...this.state, hotel: null });
  }
  handleHotelChanged(e) {
    const hotel = this.props.hotels.find(
      h => h.id === parseInt(e.target.value)
    );
    if (hotel) this.setState({ ...this.state, hotel: hotel });
    else this.setState({ ...this.state, hotel: null });
  }
  handleFinish() {
    window.sessionStorage.setItem(
      locationStepStorageKey,
      JSON.stringify(this.state)
    );
    this.props.onFinished();
  }
  render() {
    const { countries, cities, hotels, loading } = this.props;
    const isButtonDisabled =
      loading ||
      !this.state.country ||
      !this.state.city ||
      !this.state.hotel ||
      this.props.countriesLoading ||
      this.props.citiesLoading ||
      this.props.hotelsLoading;
    return (
      <div>
        <DropDown
          id="country-select"
          label="Country"
          selectedValue={this.state.country ? this.state.country.id : 0}
          onChange={this.handleCountryChanged.bind(this)}
          options={countries.map(country => ({
            value: country.id,
            name: country.name
          }))}
        />
        <DropDown
          disabled={!this.state.country}
          id="city-select"
          label="City"
          selectedValue={this.state.city ? this.state.city.id : 0}
          onChange={this.handleCityChanged.bind(this)}
          options={cities.map(city => ({
            value: city.id,
            name: city.name
          }))}
        />
        <DropDown
          disabled={!(this.state.city && this.state.country)}
          id="hotel-select"
          label="Hotel"
          selectedValue={this.state.hotel ? this.state.hotel.id : 0}
          onChange={this.handleHotelChanged.bind(this)}
          options={hotels.map(hotel => ({
            value: hotel.id,
            name: hotel.name
          }))}
        />
        <button
          disabled={isButtonDisabled}
          onClick={this.handleFinish.bind(this)}
        >
          <Translate id="select" />
        </button>
      </div>
    );
  }
}

const mapStateToProps = state => ({
  countries: countriesSelector(state),
  cities: citiesSelector(state),
  hotels: hotelsSelector(state),
  countriesLoading: countriesFetchingSelector(state),
  citiesLoading: citiesFetchingSelector(state),
  hotelsLoading: hotelsFetchingSelector(state)
});

const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      GetCountries,
      GetCities,
      GetHotels
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(ChooseLocation));
