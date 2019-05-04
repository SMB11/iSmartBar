import React, { Component } from "react";
import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import {
  rootCategorySelector,
  subCategorySelector
} from "../../redux/selectors/category";
import { cartFullCountSelector } from "../../redux/selectors/cart";
import "../../assets/scss/header.scss";
import { Link } from "react-router-dom";
class SubNavBar extends Component {
  state = {};

  render() {
    return (
      <div className="top-bar">
        <div className="left-content">
          {this.props.rootCategories.map((root, index) => (
            <div key={index} className="dropdown">
              <Link to={"/subcategory/" + root.id}>
                <button className="dropbtn">{root.name}</button>
              </Link>
              <div className="dropdown-content">
                {this.props.subCategories(root.id).map((subcategory, index) => (
                  <Link
                    key={index}
                    to={
                      "/subcategory/" +
                      root.id +
                      "?scrollTo=" +
                      subcategory.name
                    }
                  >
                    <span>{subcategory.name}</span>
                  </Link>
                ))}
              </div>
            </div>
          ))}
        </div>
        <div className="right-content">
          <Link to="/cart">
            <img src="http://localhost:3000/images/items.svg" alt="" />

            <div>
              <span>
                <Translate id="your_smartbar" />
              </span>
              <span>
                <Translate id="items" /> ({this.props.cartQuantity})
              </span>
            </div>
          </Link>
        </div>
      </div>
    );
  }
}
const mapStateToProps = (state, props) => ({
  rootCategories: rootCategorySelector(state),
  subCategories: id => subCategorySelector(state, id),
  cartQuantity: cartFullCountSelector(state)
});

export default connect(mapStateToProps)(withLocalize(SubNavBar));
