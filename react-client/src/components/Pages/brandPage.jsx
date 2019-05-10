import React, { Component } from "react";
import Product from "../Products/product";
import SideBar from "../Reusable/sidebar";

import "../../assets/scss/brand.scss";
import "../../assets/scss/products.scss";
import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { GetBrandProducts } from "../../redux/products";
import { Link } from "react-router-dom";
import NavBar from "../Reusable/navBar";
import SubNavBar from "../Reusable/subNavBar";
import {
  brandProductsSelector,
  brandProductsLoadingSelector
} from "../../redux/selectors/product";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import { assetBaseUrl } from "../../api";
import Footer from "../Reusable/footer";
import {
  categorySelector,
  categoriesLoadingSelector
} from "../../redux/selectors/category";
import { CategoryGet } from "../../redux/category";
class BrandPage extends Component {
  state = {
    hash: undefined
  };
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "brand-body";
    this.getProducts(this.props.brandID, this.props.categoryID);
  }
  componentWillReceiveProps(nextProps) {
    if (
      nextProps.brandID !== this.props.brandID ||
      nextProps.categoryID !== this.props.categoryID
    )
      this.getProducts(nextProps.brandID, nextProps.categoryID);
  }
  getProducts(brandID, categoryID) {
    const language = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.GetBrandProducts(language, brandID, categoryID);
    this.props.CategoryGet(language);
  }
  render() {
    let { products, category } = this.props;
    if (!products) products = [];
    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        <div className="content">
          <div className="breadcrumbs">
            <Link to="/">Home</Link>
            {category ? (
              <Link to={"/subcategory/" + category.parentID}>
                {category ? category.name : ""}
              </Link>
            ) : (
              ""
            )}
            <a>{this.props.brandName}</a>
          </div>
          <div className="body">
            {/* <div className="right-content"> */}
            <div
              className={
                "products  " + (this.props.loading ? "loading-wrapper" : "")
              }
            >
              <div
                className={"ui dimmer " + (this.props.loading ? "active" : "")}
              >
                <div className="ui loader" />
              </div>
              <div className="category-name">{this.props.brandName}</div>
              <div className="content">
                {products.map(prod => (
                  <Product key={prod.id} product={prod} />
                ))}
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

const mapStateToProps = (state, props) => {
  return {
    products: brandProductsSelector(
      state,
      parseInt(props.match.params.brandID),
      parseInt(props.match.params.catID)
    ),
    categoryID: parseInt(props.match.params.catID),
    brandID: parseInt(props.match.params.brandID),
    brandName: props.match.params.brand,
    category: categorySelector(state, parseInt(props.match.params.catID)),
    loading:
      brandProductsLoadingSelector(state) || categoriesLoadingSelector(state)
  };
};
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      GetBrandProducts,
      CategoryGet
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(BrandPage));
