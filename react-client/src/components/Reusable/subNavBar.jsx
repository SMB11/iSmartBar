import React, { Component } from "react";
import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import {
  rootCategorySelector,
  subCategorySelector
} from "../../redux/selectors/category";
import "../../assets/scss/header.scss";
import { Link } from "react-router-dom";
class SubNavBar extends Component {
  state = {};

  render() {
    return (
      <div class="top-bar">
        <div class="left-content">
          {this.props.rootCategories.map((root, index) => (
            <div key={index} class="dropdown">
              <Link to={"/subcategory/" + root.id}>
                <button class="dropbtn">{root.name}</button>
              </Link>
              <div class="dropdown-content">
                {this.props.subCategories(root.id).map((subcategory, index) => (
                  <a key={index}>{subcategory.name}</a>
                ))}
              </div>
            </div>
          ))}
        </div>
        <div class="right-content">
          <img src="http://localhost:3000/images/items.svg" alt="" />
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
