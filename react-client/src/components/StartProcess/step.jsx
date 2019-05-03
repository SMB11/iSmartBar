import React, { Component } from "react";

class Step extends Component {
  render() {
    const { step, stepsCount } = this.props;
    let stepsHtml = [];
    for (let i = 0; i < stepsCount; i++) {
      if (step == i + 1) stepsHtml.push(<div key={i} className="dot active" />);
      else stepsHtml.push(<div className="dot" />);
    }
    return (
      <div id="container">
        <div className="page-logo">
          <img src="images/Logo_big.png" alt="ISmartBar logo" />
        </div>
        <div className="content">
          <div className="body">
            <div className="steps">{this.props.children}</div>

            <div className="dots">{stepsHtml}</div>
          </div>
        </div>
      </div>
    );
  }
}

export default Step;
