import React, { Component } from "react";

class DropDown extends Component {
  render() {
    return (
      <div className="select-group">
        <div>
          <div className="select animated zoomIn">
            <input
              disabled={this.props.disabled}
              type="radio"
              name={this.props.id}
            />
            <i className="toggle icon icon-arrow-down" />
            <i className="toggle icon icon-arrow-up" />
            <span className="placeholder">{this.props.label}</span>
            {this.props.options.map(option => (
              <label key={option.value} className="option">
                <input
                  type="radio"
                  name={this.props.id}
                  onChange={this.props.onChange}
                  checked={
                    option.value === this.props.selectedValue ? true : false
                  }
                  value={option.value}
                />
                <span className="title animated fadeIn">{option.name}</span>
              </label>
            ))}
          </div>
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
