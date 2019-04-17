import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

class Language extends Component {
  render() {
    const { language } = this.props;
    const { id, name, checked } = language;
    return (
      <div>
        <label htmlFor={"language-radio-" + id}>{name}</label>
        <input
          onChange={this.props.onChecked}
          type="radio"
          name="languageSelect"
          value={id}
          checked={this.props.checked}
        />
      </div>
    );
  }
}

const mapStateToProps = (state, ownProps) => ownProps;

export default connect(mapStateToProps)(Language);
