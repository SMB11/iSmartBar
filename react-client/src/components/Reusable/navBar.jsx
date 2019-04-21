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
  refresh() {
    window.location.reload();
  }

  render() {
    return (
      <div>
        <header className="header">
          <a href="" className="logo">
            <img src="images/logo-manu.svg" alt="" />
          </a>
          <input className="menu-btn" type="checkbox" id="menu-btn" />
          <label className="menu-icon" htmlFor="menu-btn">
            <span className="navicon" />
          </label>
          <ul className="menu">
            <li>
              <a className="japan-letter" href="#work">
                <img src="images/japn-letter.svg" alt="" />
              </a>
            </li>
            <li>
              <a href="#about">{this.state.location}</a>
            </li>
            <li>
              <a href="#careers">Rome</a>
            </li>
            <li className="search">
              <input type="search" placeholder="Search" title="Search" />
            </li>
          </ul>
        </header>
        <Modal
          isOpen={this.state.isLanguageModalOpen}
          onRequestClose={this.closeLanguageModal.bind(this)}
        >
          <ChooseLanguage onFinished={this.refresh.bind(this)} />
        </Modal>

        <Modal
          isOpen={this.state.isLocationModalOpen}
          onRequestClose={this.closeLocationModal.bind(this)}
        >
          <ChooseLocation onFinished={this.closeLocationModal.bind(this)} />
        </Modal>
      </div>
    );
  }
}

export default navBar;
