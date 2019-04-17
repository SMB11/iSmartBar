import React, { Component } from "react";

class DropDown extends Component {
  render() {
    return (
      <div>
        <label htmlFor={this.props.id}>{this.props.label}</label>
        <select
          value={this.props.selectedValue}
          disabled={this.props.disabled}
          onChange={this.props.onChange}
          id={this.props.id}
        >
          <option />
          {this.props.options.map(option => (
            <option key={option.value} value={option.value}>
              {option.name}
            </option>
          ))}
        </select>
      </div>
    );
  }
}

export default DropDown;
