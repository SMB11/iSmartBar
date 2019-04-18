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
    window.sessionStorage.setItem(miniBarStepStorageKey, "0");
    this.props.onFinished();
  }
  handleCustomClicked() {
    window.sessionStorage.setItem(miniBarStepStorageKey, "1");
    this.props.onFinished();
  }
  render() {
    return (
      <div>
        <Link to="/checkout">
          <button onClick={this.handleDefaultClicked.bind(this)}>
            Default
          </button>
        </Link>
        <Link to="/">
          <button onClick={this.handleCustomClicked.bind(this)}>Custom</button>
        </Link>
      </div>
    );
  }
}

const mapStateToProps = state => ({});

export default connect(mapStateToProps)(withLocalize(ChooseMiniBar));
