import React, { Component } from "react";
import DatePicker from "../Reusable/datePicker";
import { Translate } from "react-localize-redux";
import moment from "moment";

export const dateStepStorageKey = "ChooseDateState";
class ChooseDate extends Component {
  state = {
    startDate: null,
    endDate: null,
    focusedInput: null
  };
  componentDidMount() {
    const state = JSON.parse(window.localStorage.getItem(dateStepStorageKey));
    if (state) {
      state.startDate = new moment(state.startDate);
      state.endDate = new moment(state.endDate);
      state.focusedInput = null;
      this.setState(state);
    }
  }
  handleFinish() {
    window.localStorage.setItem(dateStepStorageKey, JSON.stringify(this.state));
    this.props.onFinished();
  }
  render() {
    const isButtonDisabled = !this.state.endDate || !this.state.startDate;
    return (
      <div className="step step4">
        <div className="container">
          <DatePicker
            label="Choose Date"
            startDate={this.state.startDate}
            endDate={this.state.endDate}
            onChange={({ startDate, endDate }) =>
              this.setState({ startDate, endDate })
            }
            focusedInput={this.state.focusedInput}
            onFocusChange={focusedInput => this.setState({ focusedInput })}
          />
        </div>

        <div className="button-content">
          <button
            disabled={isButtonDisabled}
            onClick={this.handleFinish.bind(this)}
            className="btn"
          >
            <Translate id="select" />
          </button>
        </div>
      </div>
    );
  }
}

export default ChooseDate;
