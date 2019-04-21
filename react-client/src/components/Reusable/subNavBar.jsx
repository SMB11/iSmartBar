import React, { Component } from "react";
import { withLocalize } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import {
  rootCategorySelector,
  subCategorySelector
} from "../../redux/selectors/category";
class SubNavBar extends Component {
  state = {};

  render() {
    console.log(this.props.rootCategories);
    return (
      <div className="top-bar">
        <div className="left-content">
          {this.props.rootCategories.map((root, index) => (
            <div key={index} className="selectdiv">
              <label>
                <select>
                  <option selected>{root.name} </option>
                  {this.props
                    .subCategories(root.id)
                    .map((subcategory, index) => (
                      <option key={index}>{subcategory.name}</option>
                    ))}
                </select>
              </label>
            </div>
          ))}
        </div>

        <div className="right-content">
          <img src="images/items.svg" alt="" />
          <div>
            <span>Your iSmartBar</span>
            <span>Items (0)</span>
          </div>
        </div>
      </div>
    );
  }
}
const mapStateToProps = (state, props) => ({
  rootCategories: rootCategorySelector(state),
  subCategories: id => subCategorySelector(state, id)
});

export default connect(mapStateToProps)(withLocalize(SubNavBar));
