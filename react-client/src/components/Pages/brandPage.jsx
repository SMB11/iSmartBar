import React, { Component } from "react";
import Product from "../Products/product";
import SideBar from "../Reusable/sidebar";

import "../../assets/scss/brand.scss";
import "../../assets/scss/products.scss";
import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { GetBrandProducts } from "../../redux/products";
import NavBar from "../Reusable/navBar";
import SubNavBar from "../Reusable/subNavBar";
import { brandProductsSelector } from "../../redux/selectors/product";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import { assetBaseUrl } from "../../api";
import Footer from "../Reusable/footer";
class BrandPage extends Component {
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "brand-body";
    this.getProducts();
  }
  componentDidUpdate() {
    this.getProducts();
  }
  getProducts() {
    const language = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.GetBrandProducts(language, parseInt(this.props.match.params.id));
  }
  render() {
    let products = this.props.products;
    if (!products) products = [];
    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        <div class="content">
          <div class="breadcrumbs">
            <a href="">Category</a>
            <a href="">Subcategory</a>
            <a href="">Brand</a>
          </div>
          <div class="body">
            <div class="left-content">
              <SideBar>
                <div class="filter">
                  <div class="title">Subcategory</div>
                  <div>
                    <p>
                      <label for="test1">Sub 1</label>
                    </p>
                    <p>
                      <label for="test2">Sub 2</label>
                    </p>
                    <p>
                      <label for="test3">Sub 3</label>
                    </p>
                    <p>
                      <label for="test4">Sub 4</label>
                    </p>
                    <p>
                      <label for="test5">Sub 5</label>
                    </p>
                  </div>
                </div>
              </SideBar>
            </div>

            <div class="right-content">
              <div class="products">
                <div class="category-name">Tequila</div>
                <div class="content">
                  {products.map(prod => (
                    <Product product={prod} />
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>
        <Footer />
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state, props) => {
  return {
    products: brandProductsSelector(state, parseInt(props.match.params.id))
  };
};
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      GetBrandProducts
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(BrandPage));
