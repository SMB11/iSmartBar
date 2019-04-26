import React, { Component } from "react";
import "../../assets/scss/select.scss";
import SelectFX from "periodicjs.component.selectfx";
class DropDown extends Component {
  componentDidUpdate() {
    console.log(this.refs.select);
  }
  render() {
    return (
      <section>
        <select
          ref="select"
          value={this.props.selectedValue}
          disabled={this.props.disabled}
          onChange={this.props.onChange}
          id={this.props.id}
          className="cs-select cs-skin-elastic"
        >
          <option value="" disabled selected>
            {this.props.label}
          </option>

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
      </section>

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
