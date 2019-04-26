import React, { Component } from "react";

import "../../assets/scss/select.scss";
import "../../assets/scss/steps.scss";
import Step from "./step";
import ChooseLanguage, { languageStepStorageKey } from "./chooseLanguage";
import ChooseLocation from "./chooseLocation";
import ChooseMiniBar from "./chooseMiniBar";
import { Translate } from "react-localize-redux";
import { Redirect } from "react-router-dom";
class StartProcessPage extends Component {
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "steps-body";
  }
  state = {
    steps: [
      <ChooseLanguage onFinished={this.handleStepFinished.bind(this)} />,
      <ChooseLocation onFinished={this.handleStepFinished.bind(this)} />
    ],
    step: 1,
    redirectHome: false
  };

  handleStepFinished() {
    const step = this.state.step;

    console.log("Step " + step + " Finished");
    if (step !== this.state.steps.length) {
      this.setState({ ...this.state, step: step + 1 });
    } else {
      // this.setState({ ...this.state, redirectHome: true });
      window.location.href = "http://localhost:3000/";
    }
  }
  backButtonHandler = () => {
    const step = this.state.step;

    if (this.state.step - 1 !== 0) {
      this.setState({ ...this.state, step: step - 1 });
      // console.log(this.state);
    }
  };

  render() {
    const step = this.state.step;
    return (
      <div>
        <Step step={step} stepsCount={this.state.steps.length}>
          {this.state.steps[step - 1]}
        </Step>
        {this.state.step - 1 !== 0 ? (
          <button onClick={this.backButtonHandler}>
            <Translate id="back" />
          </button>
        ) : null}
      </div>
    );
  }
}

export default StartProcessPage;
