import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  withLocalize,
  Translate,
  setActiveLanguage
} from "react-localize-redux";

class ChooseMiniBar extends Component {
  render() {
    return (
      <div>
        <a href="/default">
          <button onClick={this.props.onFinished}>Default</button>
        </a>
        <a href="/custom">
          <button onClick={this.props.onFinished}>Custom</button>
        </a>
      </div>
    );
  }
}

const mapStateToProps = state => ({});

export default connect(mapStateToProps)(withLocalize(ChooseMiniBar));
