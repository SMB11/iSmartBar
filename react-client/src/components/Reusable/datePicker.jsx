import React, { Component } from "react";
import { DateRangePicker } from "react-dates";
import "react-dates/initialize";
import "react-dates/lib/css/_datepicker.css";
import "../../assets/scss/datePicker.scss";
import isMobile from "ismobilejs";
class DatePicker extends Component {
  render() {
    return (
      <div className="select-group">
        <div className="select-label"> {this.props.label}</div>

        <DateRangePicker
          orientation={isMobile.any ? "vertical" : "horizontal"}
          verticalHeight={isMobile.any ? 350 : undefined}
          numberOfMonths={isMobile.any ? 1 : undefined}
          isReadOnly={true}
          readOnly={true}
          // withPortal={true}
          showClearDates={true}
          startDate={this.props.startDate} // momentPropTypes.momentObj or null,
          startDateId="startDateID" // PropTypes.string.isRequired,
          endDate={this.props.endDate} // momentPropTypes.momentObj or null,
          endDateId="endDateID" // PropTypes.string.isRequired,
          onDatesChange={this.props.onChange}
          focusedInput={this.props.focusedInput} // PropTypes.oneOf([START_DATE, END_DATE]) or null,
          onFocusChange={this.props.onFocusChange} // PropTypes.func.isRequired,
        />
      </div>
    );
  }
}

export default DatePicker;
