import React, { Component } from "react";
import { stringify } from "querystring";

export const locationStepStorageKey = "ChooseLocationState";
export const languageStepStorageKey = "ChooseLanguageState";
class navBar extends Component {
  state = {
    language: null,
    location: null
  };

  componentDidMount() {
    const cityObj = JSON.parse(
      window.sessionStorage.getItem(locationStepStorageKey)
    );
    const languageObj = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    );
    const city = cityObj.city.name;
    const hotel = cityObj.hotel.name;
    const location = hotel + ":" + city;
    const language = languageObj.selected.name;
    console.log(language);
    let newState = {
      language: language,
      location: location
    };
    this.setState(newState);
  }
  render() {
    return (
      <nav>
        <h1>Logo</h1>
        <a href="/">{this.state.language} </a>
        <a href="/">{this.state.location}</a>
        <div>
          <input type="text" />
          <button>Search</button>
        </div>
      </nav>
    );
  }
}

export default navBar;
