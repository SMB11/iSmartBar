import React, { Component } from "react";
import Autosuggest from "react-autosuggest";
import isMobile from "ismobilejs";
const getSuggestionValue = suggestion => suggestion.name;

const getSuggestions = (suggestions, value) => {
  if (!value) return [];
  const inputValue = value.trim().toLowerCase();
  const inputLength = inputValue.length;

  return inputLength === 0
    ? []
    : suggestions.filter(
        option => option.name.toLowerCase().slice(0, inputLength) === inputValue
      );
};
const renderSuggestion = suggestion => <div>{suggestion.name}</div>;
class AutoSuggestDropdown extends Component {
  state = {
    value: "",
    suggestions: []
  };

  onChange = (event, { newValue }) => {
    this.setState({
      value: newValue
    });
    this.props.onChange(newValue);
  };
  onSuggestionsFetchRequested = ({ value }) => {
    this.setState({
      suggestions: getSuggestions(this.props.options, value)
    });
  };
  onSuggestionsClearRequested = () => {
    this.setState({
      suggestions: []
    });
  };
  render() {
    let { suggestions, value } = this.state;
    const { selectedValue } = this.props;
    if (!value && selectedValue) value = selectedValue;
    const inputProps = {
      value,
      onChange: this.onChange
    };
    return (
      <div
        className={"select-group" + (this.props.disabled ? " disabled" : "")}
      >
        <div className="select-label"> {this.props.label}</div>
        <div className="select-wrapper">
          <div
            className={
              "ui dimmer inverted " + (this.props.loading ? "active" : "")
            }
          >
            <div className="ui loader" />
          </div>
          <Autosuggest
            suggestions={suggestions}
            onSuggestionsFetchRequested={this.onSuggestionsFetchRequested}
            onSuggestionsClearRequested={this.onSuggestionsClearRequested}
            focusInputOnSuggestionClick={!isMobile.any}
            getSuggestionValue={getSuggestionValue}
            renderSuggestion={renderSuggestion}
            inputProps={inputProps}
          />
        </div>
      </div>
    );
  }
}

export default AutoSuggestDropdown;
