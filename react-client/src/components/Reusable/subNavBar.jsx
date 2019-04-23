import React, { Component } from "react";
import { withLocalize, Translate } from "react-localize-redux";
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
      <div class="top-bar">
        <div class="left-content">
          {this.props.rootCategories.map((root, index) => (
            <div key={index} class="dropdown">
              <button class="dropbtn">{root.name}</button>
              <div class="dropdown-content">
                {this.props.subCategories(root.id).map((subcategory, index) => (
                  <a key={index}>{subcategory.name}</a>
                ))}
              </div>
            </div>
          ))}
        </div>
        <div class="right-content">
          <img src="images/items.svg" alt="" />
          <div>
            <span>
              <Translate id="your_smartbar" />
            </span>
            <span>
              <Translate id="items" /> (0)
            </span>
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
