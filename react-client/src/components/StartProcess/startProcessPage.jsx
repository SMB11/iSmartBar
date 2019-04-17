import React, { Component } from "react";
import Step from "./step";
import ChooseLanguage from "./chooseLanguage";
import ChooseLocation from "./chooseLocation";
import ChooseMiniBar from "./chooseMiniBar";

class StartProcessPage extends Component {
  state = {
    steps: [
      <ChooseLanguage onFinished={this.handleStepFinished.bind(this)} />,
      <ChooseLocation onFinished={this.handleStepFinished.bind(this)} />,
      <ChooseMiniBar onFinished={this.handleStepFinished.bind(this)} />
    ],
    step: 1
  };

  handleStepFinished() {
    const step = this.state.step;

    console.log("Step " + step + " Finished");
    if (step != this.state.steps.length) {
      this.setState({ ...this.state, step: step + 1 });
    } else {
      console.log("All Finished");
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
          <button onClick={this.backButtonHandler}>Back</button>
        ) : null}
      </div>
    );
  }
}

export default StartProcessPage;
