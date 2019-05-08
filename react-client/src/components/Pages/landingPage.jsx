import React, { Component } from "react";
import NavBar from "../Reusable/navBar";
import Product from "../Products/product";
import api from "../../api";
import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { CategoryGet } from "../../redux/category";
import { GetTopFive } from "../../redux/products";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import "../../assets/scss/home.scss";
import SubNavBar from "../Reusable/subNavBar";
import { rootCategorySelector } from "../../redux/selectors/category";
import Footer from "../Reusable/footer";

import {} from "react-router";
import {
  topFiveSelector,
  topFiveLoadingSelector
} from "../../redux/selectors/product";
import ProductHome from "../Products/productHome";
class landingPage extends Component {
  redirect(target) {
    this.props.history.push(target);
  }

  state = {};
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "landing-body";
    this.apiCalls();
  }
  apiCalls() {
    const language = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.GetTopFive(language);
    this.props.CategoryGet(language);
  }

  render() {
    return (
      <React.Fragment>
        {/* <div id="container"> */}
        <NavBar />
        <SubNavBar />
        <div className="content">
          <div className="homepage-img">
            <img className="homepage-img" src="images/home-image.png" alt="" />
          </div>
        </div>
        <div className="categories-content" id="category">
          <div className="title">
            <h2>
              <Translate id="categories" />
            </h2>
          </div>
          <div className="line-horizontal" />
          <div className="categories">
            <div className="categories-row">
              <div
                className="category"
                onClick={() =>
                  this.redirect(
                    this.props.rootCategories[0]
                      ? "/subcategory/" + this.props.rootCategories[0].id
                      : null
                  )
                }
              >
                <h3 className="title">
                  <div className="titleBefore" />
                  {this.props.rootCategories[0]
                    ? this.props.rootCategories[0].name
                    : ""}
                  <div className="titleAfter" />
                </h3>
                <div className="category-content">
                  <div className="category-image">
                    <img src="images/food.svg" alt="" />
                  </div>
                  <div className="description">
                    <span>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                      sed do eiusmod tempor incididunt ut labore et dolore magna
                      aliqua. Ut enim ad minim veniam, quis nostrud
                    </span>
                  </div>
                </div>
              </div>
              <div
                className="category"
                onClick={() =>
                  this.redirect(
                    this.props.rootCategories[1]
                      ? "/subcategory/" + this.props.rootCategories[1].id
                      : null
                  )
                }
              >
                <h3 className="title">
                  <div className="titleBefore" />
                  {this.props.rootCategories[1]
                    ? this.props.rootCategories[1].name
                    : ""}

                  <div className="titleAfter" />
                </h3>
                <div className="category-content">
                  <div className="category-image">
                    <img src="images/drink.svg" alt="" />
                  </div>
                  <div className="description">
                    <span>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                      sed do eiusmod tempor incididunt ut labore et dolore magna
                      aliqua. Ut enim ad minim veniam, quis nostrud
                    </span>
                  </div>
                </div>
              </div>
              <div
                className="category"
                onClick={() =>
                  this.redirect(
                    this.props.rootCategories[2]
                      ? "/subcategory/" + this.props.rootCategories[2].id
                      : null
                  )
                }
              >
                <h3 className="title">
                  <div className="titleBefore" />
                  {this.props.rootCategories[2]
                    ? this.props.rootCategories[2].name
                    : ""}

                  <div className="titleAfter" />
                </h3>
                <div className="category-content">
                  <div className="category-image">
                    <img src="images/kids.svg" alt="" />
                  </div>
                  <div className="description">
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
        <div className="content">
          <div className="most-reviewed">
            <div className="title">
              <h2>
                <Translate id="most_reviewed_products" />
              </h2>
            </div>
            <div className="products-columns">
              <div className="products-column">
                <ProductHome product={this.props.topFive[0]} />
                <ProductHome product={this.props.topFive[1]} />
              </div>
              <div className="products-column middle">
                <ProductHome product={this.props.topFive[2]} />
              </div>
              <div className="products-column">
                <ProductHome product={this.props.topFive[3]} />
                <ProductHome product={this.props.topFive[4]} />
              </div>
            </div>
          </div>
        </div>
        {/* </div> */}

        <Footer />
      </React.Fragment>
    );
  }
}
const mapStateToProps = (state, props) => ({
  rootCategories: rootCategorySelector(state),
  topFive: topFiveSelector(state),
  topFiveLaoding: topFiveLoadingSelector(state)
});
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      CategoryGet,
      GetTopFive
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(landingPage));
