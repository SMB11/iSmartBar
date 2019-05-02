import React, { Component } from "react";
import NavBar from "../Reusable/navBar";
import Product from "../Products/product";
import api from "../../api";
import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { CategoryGet } from "../../redux/category";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import "../../assets/scss/home.scss";
import SubNavBar from "../Reusable/subNavBar";
import { rootCategorySelector } from "../../redux/selectors/category";
import Footer from "../Reusable/footer";

import {} from "react-router";
class landingPage extends Component {
  redirect(target) {
    this.props.history.push(target);
  }

  state = {};
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "landing-body";
  }

  render() {
    return (
      <React.Fragment>
        {/* <div id="container"> */}
        <NavBar />
        <SubNavBar />
        <div class="content">
          <div class="homepage-img">
            <img class="homepage-img" src="images/homepage-img.svg" alt="" />
          </div>
        </div>
        <div class="categories-content">
          <div class="title">
            <h2>
              <Translate id="categories" />
            </h2>
          </div>
          <div class="line-horizontal" />
          <div class="categories">
            <div class="categories-row">
              <div
                class="category"
                onClick={() =>
                  this.redirect(
                    this.props.rootCategories[0]
                      ? "/subcategory/" + this.props.rootCategories[0].id
                      : null
                  )
                }
              >
                <h3 class="title">
                  <div className="titleBefore" />
                  {this.props.rootCategories[0]
                    ? this.props.rootCategories[0].name
                    : ""}
                  <div className="titleAfter" />
                </h3>
                <div class="category-content">
                  <div class="category-image">
                    <img src="images/food.svg" alt="" />
                  </div>
                  <div class="description">
                    <span>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                      sed do eiusmod tempor incididunt ut labore et dolore magna
                      aliqua. Ut enim ad minim veniam, quis nostrud
                    </span>
                  </div>
                </div>
              </div>
              <div
                class="category"
                onClick={() =>
                  this.redirect(
                    this.props.rootCategories[1]
                      ? "/subcategory/" + this.props.rootCategories[1].id
                      : null
                  )
                }
              >
                <h3 class="title">
                  <div className="titleBefore" />
                  {this.props.rootCategories[1]
                    ? this.props.rootCategories[1].name
                    : ""}

                  <div className="titleAfter" />
                </h3>
                <div class="category-content">
                  <div class="category-image">
                    <img src="images/drink.svg" alt="" />
                  </div>
                  <div class="description">
                    <span>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                      sed do eiusmod tempor incididunt ut labore et dolore magna
                      aliqua. Ut enim ad minim veniam, quis nostrud
                    </span>
                  </div>
                </div>
              </div>
              <div
                class="category"
                onClick={() =>
                  this.redirect(
                    this.props.rootCategories[2]
                      ? "/subcategory/" + this.props.rootCategories[2].id
                      : null
                  )
                }
              >
                <h3 class="title">
                  <div className="titleBefore" />
                  {this.props.rootCategories[2]
                    ? this.props.rootCategories[2].name
                    : ""}

                  <div className="titleAfter" />
                </h3>
                <div class="category-content">
                  <div class="category-image">
                    <img src="images/kids.svg" alt="" />
                  </div>
                  <div class="description">
                    <span>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                      sed do eiusmod tempor incididunt ut labore et dolore magna
                      aliqua. Ut enim ad minim veniam, quis nostrud
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="content">
          <div class="most-reviewed">
            <div class="title">
              <h2>
                <Translate id="most_reviewed_products" />
              </h2>
            </div>
            <div class="products-columns">
              <div class="products-column">
                <div class="product">
                  <div class="product-image">
                    <img src="images/product1.svg" alt="" />
                  </div>
                  <div class="info">
                    <p>Category</p>
                    <p>Name</p>
                    <p>$ XX.xx</p>
                  </div>
                </div>
                <div class="product">
                  <div class="product-image">
                    <img src="images/product2.svg" alt="" />
                  </div>
                  <div class="info">
                    <p>Category</p>
                    <p>Name</p>
                    <p>$ XX.xx</p>
                  </div>
                </div>
              </div>
              <div class="products-column">
                <div class="product">
                  <div class="product-image">
                    <img src="images/big-product.svg" alt="" />
                  </div>
                  <div class="info">
                    <p>Category</p>
                    <p>Name</p>
                    <p>$ XX.xx</p>
                  </div>
                </div>
              </div>
              <div class="products-column">
                <div class="product">
                  <div class="product-image">
                    <img src="images/product3.svg" alt="" />
                  </div>
                  <div class="info">
                    <p>Category</p>
                    <p>Name</p>
                    <p>$ XX.xx</p>
                  </div>
                </div>
                <div class="product">
                  <div class="product-image">
                    <img src="images/product1.svg" alt="" />
                  </div>
                  <div class="info">
                    <p>Category</p>
                    <p>Name</p>
                    <p>$ XX.xx</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        {/* </div> */}
      </React.Fragment>
    );
  }
}
const mapStateToProps = (state, props) => ({
  rootCategories: rootCategorySelector(state)
});
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      CategoryGet
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(landingPage));
