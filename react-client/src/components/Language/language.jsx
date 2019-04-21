import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

class Language extends Component {
  render() {
    const { language } = this.props;
    const { id, name, checked } = language;
    return (
      <React.Fragment>
        <label className="container-radio">
          {name}
          <input
            onChange={this.props.onChecked}
            value={id}
            checked={this.props.checked}
            type="radio"
            name="lng"
          />
          <span className="checkmark" />
        </label>
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state, ownProps) => ownProps;

export default connect(mapStateToProps)(Language);
