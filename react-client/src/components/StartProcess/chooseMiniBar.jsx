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
        <button onClick={this.props.onFinished}>Default</button>
        <button onClick={this.props.onFinished}>Custom</button>
      </div>
    );
  }
}

const mapStateToProps = state => ({});

export default connect(mapStateToProps)(withLocalize(ChooseMiniBar));
