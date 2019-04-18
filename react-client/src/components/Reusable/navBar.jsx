import React, { Component } from "react";
import { stringify } from "querystring";
import ChooseLocation, {
  locationStepStorageKey
} from "../StartProcess/chooseLocation";
import ChooseLanguage, {
  languageStepStorageKey
} from "../StartProcess/chooseLanguage";
import { Link } from "react-router-dom";
import Modal from "react-modal";

class navBar extends Component {
  state = {
    language: null,
    location: null,
    isLanguageModalOpen: false,
    isLocationModalOpen: false
  };

  componentDidMount() {
    this.loadFromStorage();
  }

  loadFromStorage() {
    const cityObj = JSON.parse(
      window.sessionStorage.getItem(locationStepStorageKey)
    );
    const languageObj = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    );
    const city = cityObj.city.name;
    const hotel = cityObj.hotel.name;
    const location = hotel + ", " + city;
    const language = languageObj.selected.name;
    console.log(language);
    let newState = {
      language: language,
      location: location
    };
    this.setState(newState);
  }

  openLanguageModal() {
    this.setState({ isLanguageModalOpen: true });
  }
  closeLanguageModal() {
    this.setState({ isLanguageModalOpen: false });
    this.loadFromStorage();
  }

  openLocationModal() {
    this.setState({ isLocationModalOpen: true });
  }
  closeLocationModal() {
    this.setState({ isLocationModalOpen: false });
    this.loadFromStorage();
  }

  render() {
    return (
      <nav>
        <h1>Logo</h1>
        <a onClick={this.openLanguageModal.bind(this)}>{this.state.language}</a>
        <a onClick={this.openLocationModal.bind(this)}>{this.state.location}</a>
        <div>
          <input type="text" />
          <button>Search</button>
        </div>
        <Modal
          isOpen={this.state.isLanguageModalOpen}
          onRequestClose={this.closeLanguageModal.bind(this)}
        >
          <ChooseLanguage onFinished={this.closeLanguageModal.bind(this)} />
        </Modal>

        <Modal
          isOpen={this.state.isLocationModalOpen}
          onRequestClose={this.closeLocationModal.bind(this)}
        >
          <ChooseLocation onFinished={this.closeLocationModal.bind(this)} />
        </Modal>
      </nav>
    );
  }
}

export default navBar;
