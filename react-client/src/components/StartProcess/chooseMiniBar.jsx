import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  withLocalize,
  Translate,
  setActiveLanguage
} from "react-localize-redux";
import { Link } from "react-router-dom";

export const miniBarStepStorageKey = "MiniBarType";

class ChooseMiniBar extends Component {
  handleDefaultClicked() {
    window.localStorage.setItem(miniBarStepStorageKey, "0");
    this.props.onFinished();
  }
  handleCustomClicked() {
    window.localStorage.setItem(miniBarStepStorageKey, "1");
    this.props.onFinished();
  }
  render() {
    return (
      <div className="step step1 active">
        <div className="bars">
          <div className="bar">
            <div className="bar-logo">
              <img src="images/default-bar.svg" alt="" />
            </div>
            <h2 className="title">Default Bar</h2>
            <span className="description">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam, quis nostrud exercitation ullamco laboris
              nisi ut aliquip ex ea commodo.
            </span>
            <div className="button-content">
              <Link to="/checkout">
                <button
                  onClick={this.handleDefaultClicked.bind(this)}
                  className="btn next-step"
                >
                  Default
                </button>
              </Link>
            </div>
          </div>
          <div className="line" />
          <div className="bar">
            <div className="bar-logo">
              <img src="images/custom-bar.svg" alt="" />
            </div>
            <h2 className="title">Custom Bar</h2>
            <span className="description">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
              eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
              enim ad minim veniam, quis nostrud exercitation ullamco laboris
              nisi ut aliquip ex ea commodo.
            </span>
            <div className="button-content">
              <Link to="/">
                <button
                  onClick={this.handleCustomClicked.bind(this)}
                  className="btn next-step"
                >
                  Custom
                </button>
              </Link>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = state => ({});

export default connect(mapStateToProps)(withLocalize(ChooseMiniBar));
