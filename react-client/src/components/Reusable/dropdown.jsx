import React, { Component } from "react";
import "../../assets/scss/select.scss";
import SelectFX from "periodicjs.component.selectfx";
class DropDown extends Component {
  componentWillUpdate() {}
  render() {
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
          <select
            ref="select"
            value={this.props.selectedValue}
            disabled={this.props.disabled}
            onChange={this.props.onChange}
            id={this.props.id}
          >
            <option value="" />
            {this.props.options.map(option => (
              <option
                disabled={option.disabled}
                key={option.value}
                value={option.value}
              >
                {option.name}
              </option>
            ))}
          </select>
          <i className="fas fa-chevron-down" />
        </div>
      </div>

      // <div>
      //   <label htmlFor={this.props.id}>{this.props.label}</label>
      //   <select
      //     value={this.props.selectedValue}
      //     disabled={this.props.disabled}
      //     onChange={this.props.onChange}
      //     id={this.props.id}
      //   >
      //     <option />
      //     {this.props.options.map(option => (
      //       <option key={option.value} value={option.value}>
      //         {option.name}
      //       </option>
      //     ))}
      //   </select>
      // </div>
    );
  }
}

export default DropDown;
