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
class landingPage extends Component {
  state = {};
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "landing-body";
  }

  render() {
    return (
      <div id="container">
        <NavBar />
        <SubNavBar />
        <div class="content">
          <div class="homepage-img">
            <img class="homepage-img" src="images/homepage-img.svg" alt="" />
          </div>
          <div class="categories-content">
            <div class="title">
              <h2>
                <Translate id="categories" />
              </h2>
            </div>
            <div class="line-horizontal" />
            <div class="categories">
              <div class="category">
                <h3 class="title">
                  {this.props.rootCategories[0]
                    ? this.props.rootCategories[0].name
                    : ""}
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
              <div class="category">
                <h3 class="title">
                  {this.props.rootCategories[1]
                    ? this.props.rootCategories[1].name
                    : ""}
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

              <div class="category">
                <h3 class="title">
                  {this.props.rootCategories[2]
                    ? this.props.rootCategories[2].name
                    : ""}
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
        <footer>
          <div class="footer-content">
            <div class="footer-menu">
              <ul>
                <li>
                  <a href="#">Home</a>
                </li>
                <li>
                  <a href="#">About</a>
                </li>
                <li>
                  <a href="#">Categories</a>
                </li>
                <li>
                  <a href="#">iSmartBar</a>
                </li>
                <li>
                  <a href="#">Contact</a>
                </li>
              </ul>
            </div>
            <div class="footer-line" />
            <div class="social-icons">
              <a href="#">
                <img src="images/gmail.svg" alt="gmail" />
              </a>
              <a href="#">
                <img src="images/ln.svg" alt="ln" />
              </a>
              <a href="#">
                <img src="images/twitter.svg" alt="twitter" />
              </a>
              <a href="#">
                <img src="images/fb.svg" alt="fb" />
              </a>
              <a href="#">
                <img src="images/instagram.svg" alt="instagram" />
              </a>
            </div>
            <div class="footer-info">
              <a href="mailto:info@companyname.com">
                Email: info@companyname.com
              </a>
              <a href="tel:+37400000000">Phone: (374) xxx xxxx</a>
              <a href="">Address: Biagio Capone, Via Castagneto 2, Italy</a>
            </div>
            <div class="short-line" />
            <div class="copyright">
              <span>2019. iSmartBar. All rights reserved</span>
            </div>
          </div>
        </footer>
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
