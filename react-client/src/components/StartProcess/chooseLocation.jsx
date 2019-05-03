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
import { languageStepStorageKey } from "./chooseLanguage";

export const locationStepStorageKey = "ChooseLocationState";

class ChooseLocation extends Component {
  state = {
    country: null,
    city: null,
    hotel: null,
    lang: "en"
  };
  componentDidMount() {
    const language = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.setState({ ...state, lang: language });

    this.props.GetCountries(language);
    const state = JSON.parse(
      window.localStorage.getItem(locationStepStorageKey)
    );
    if (state) {
      this.props.GetCountries(language);
      this.props.GetCities(state.country.id, language);
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
      this.setState({
        ...this.state,
        country: country,
        city: null,
        hotel: null
      });
      this.props.GetCities(country.id, this.state.lang);
    } else {
      this.setState({ ...this.state, country: null, city: null, hotel: null });
    }
  }

  handleCityChanged(e) {
    const city = this.props.cities.find(
      cnt => cnt.id === parseInt(e.target.value)
    );
    if (city) {
      this.setState({ ...this.state, city: city, hotel: null });
      this.props.GetHotels(city.id);
    } else {
      this.setState({ ...this.state, city: null, hotel: null });
    }
  }
  handleHotelChanged(e) {
    const hotel = this.props.hotels.find(
      h => h.id === parseInt(e.target.value)
    );
    if (hotel) this.setState({ ...this.state, hotel: hotel });
    else this.setState({ ...this.state, hotel: null });
  }
  handleFinish() {
    window.localStorage.setItem(
      locationStepStorageKey,
      JSON.stringify(this.state)
    );
    this.props.onFinished();
  }
  render() {
    const { countries, cities, hotels } = this.props;
    let isButtonDisabled =
      !this.state.country ||
      !this.state.city ||
      !this.state.hotel ||
      this.props.countriesLoading ||
      this.props.citiesLoading ||
      this.props.hotelsLoading;
    // isButtonDisabled = false;
    return (
      <div className="step step2">
        <form action="" className="container">
          <div className="select-form">
            <DropDown
              id="country"
              label="Country"
              selectedValue={this.state.country ? this.state.country.id : 0}
              onChange={this.handleCountryChanged.bind(this)}
              loading={this.props.countriesLoading}
              options={countries.map(country => ({
                value: country.id,
                name: country.name,
                disabled: country.id !== 1
              }))}
            />
            <DropDown
              disabled={!this.state.country}
              id="city"
              label="City"
              selectedValue={this.state.city ? this.state.city.id : 0}
              loading={this.props.citiesLoading}
              onChange={this.handleCityChanged.bind(this)}
              options={cities.map(city => ({
                value: city.id,
                name: city.name,
                disabled: city.id !== 1
              }))}
            />
            <DropDown
              disabled={!(this.state.city && this.state.country)}
              id="hotel"
              label="Hotel"
              selectedValue={this.state.hotel ? this.state.hotel.id : 0}
              loading={this.props.hotelsLoading}
              onChange={this.handleHotelChanged.bind(this)}
              options={hotels.map(hotel => ({
                value: hotel.id,
                name: hotel.name
              }))}
            />
          </div>
        </form>
        <div className="button-content">
          <button
            disabled={isButtonDisabled}
            onClick={this.handleFinish.bind(this)}
            className="btn"
          >
            <Translate id="select" />
          </button>
        </div>
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
