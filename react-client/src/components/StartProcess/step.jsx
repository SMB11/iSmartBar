import React, { Component } from "react";

class Step extends Component {
  render() {
    const { step, stepsCount } = this.props;
    return (
      <div>
        <h1>Logo</h1>
        <div>{this.props.children}</div>
        <div>
          {step}/{stepsCount}
        </div>
      </div>
    );
  }
}

export default Step;
