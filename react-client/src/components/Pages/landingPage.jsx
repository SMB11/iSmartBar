import React, { Component } from "react";
import NavBar from "../Reusable/navBar";
import Product from "../Products/product";
import api from "../../api";
import { withLocalize } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { CategoryGet } from "../../redux/category";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import "../../assets/scss/home.scss";
import SubNavBar from "../Reusable/subNavBar";
import { rootCategorySelector } from "../../redux/selectors/category";
class landingPage extends Component {
  state = {};
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "landing-body";
    console.log(this.props);
    // api.categories.getAll(this.props.activeLanguage.code).then(data => {
    //   console.log(data);
    // });
    const language = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.CategoryGet(language);
  }

  render() {
    return (
      <div id="container">
        <NavBar />
        <SubNavBar />
        {/* <Product
          product={{
            id: 1,
            name: "vardanik",
            price: 1
          }}
        /> */}
        <div className="content">
          <div className="homepage-img">
            <img
              className="homepage-img"
              src="images/homepage-img.svg"
              alt=""
            />
          </div>
          <div className="categories-content">
            <div className="title">
              <h2>Categories</h2>
            </div>
            <div className="line-horizontal" />
            <div className="categories">
              <div className="category">
                <h3 className="title">
                  {this.props.rootCategories[0]
                    ? this.props.rootCategories[0].name
                    : ""}
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
              <div className="category">
                <h3 className="title">
                  {this.props.rootCategories[1]
                    ? this.props.rootCategories[1].name
                    : ""}
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

              <div className="category">
                <h3 className="title">Kids</h3>
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
          <div className="most-reviewed">
            <div className="title">
              <h2>Most Reviewed Products</h2>
            </div>
          </div>
        </div>
      </div>
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
