import React, { Component } from "react";
// import { stringify } from "querystring";
import ChooseLocation, {
  locationStepStorageKey
} from "../StartProcess/chooseLocation";
import ChooseLanguage, {
  languageStepStorageKey
} from "../StartProcess/chooseLanguage";
import { Link } from "react-router-dom";
import Modal from "react-modal";
import { withLocalize } from "react-localize-redux";
import "../../assets/scss/header.scss";

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
      window.localStorage.getItem(locationStepStorageKey)
    );
    const languageObj = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
    );
    const city = cityObj.city.name;
    const hotel = cityObj.hotel.name;
    const location = hotel + ", " + city;
    const language = languageObj.selected.name;
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
      <React.Fragment>
        <header className="header">
          <Link to="/" className="logo">
            <img src="http://localhost:3000/images/logo.png" alt="" />
          </Link>
          <input className="menu-btn" type="checkbox" id="menu-btn" />
          <label className="menu-icon" htmlFor="menu-btn">
            <span className="navicon" />
          </label>
          <ul className="menu">
            <li>
              <a
                className="japan-letter"
                onClick={this.openLanguageModal.bind(this)}
              >
                <img src="http://localhost:3000/images/language.svg" alt="" />
              </a>
            </li>
            <li>
              <a
                className="locationEdit"
                onClick={this.openLocationModal.bind(this)}
              >
                {this.state.location}
              </a>
            </li>
            <li className="search">
              <input
                type="search"
                placeholder={this.props.translate("search")}
                title="Search"
              />
            </li>
          </ul>
        </header>
        <Modal
          className="Modal"
          overlayClassName="Overlay"
          isOpen={this.state.isLanguageModalOpen}
          onRequestClose={this.closeLanguageModal.bind(this)}
        >
          <div className="steps">
            <ChooseLanguage onFinished={this.refresh.bind(this)} />
          </div>
        </Modal>

        <Modal
          className="Modal"
          overlayClassName="Overlay"
          isOpen={this.state.isLocationModalOpen}
          onRequestClose={this.closeLocationModal.bind(this)}
        >
          <div className="steps">
            <ChooseLocation onFinished={this.closeLocationModal.bind(this)} />
          </div>
        </Modal>
      </React.Fragment>
    );
  }
}

export default withLocalize(navBar);
