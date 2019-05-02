import React, { Component } from "react";
import "../../assets/scss/subcategory.scss";
import SubNavBar from "../Reusable/subNavBar";
import NavBar from "../Reusable/navBar";
import {
  rootCategoriesBrandsSelector,
  rootCategoriesBrandsLoadingSelector,
  categorySelector
} from "../../redux/selectors/category";
import category, { RootCategoryBrandGet } from "../../redux/category";

import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import Brand from "../Brands/brand";
import SideBar from "../Reusable/sidebar";
import Footer from "../Reusable/footer";
class SubCategoriesPage extends Component {
  state = {
    hash: undefined
  };
  getBrands = () => {
    this.state.hash = window.location.hash.substr(1);
    const language = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.RootCategoryBrandGet(
      language,
      parseInt(this.props.match.params.id)
    );
  };
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "subcategory-body";
    this.getBrands();
  }
  componentDidUpdate() {
    this.getBrands();
  }
  render() {
    let { categoryBrands, category } = this.props;

    if (!categoryBrands) categoryBrands = {};
    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        <div className="content">
          <div className="breadcrumbs">
            <Link to="/">Home</Link>
            <Link>{category ? category.name : ""}</Link>
          </div>
          <div className="body">
            <div className="left-content">
              <SideBar>
                <div className="filter">
                  <div className="title">Subcategory</div>
                  <div>
                    {Object.keys(categoryBrands).map(catName => (
                      <p>
                        <a href={"#" + catName}>{catName}</a>
                      </p>
                    ))}
                  </div>
                </div>
              </SideBar>
            </div>
            <div className="right-content loading-wrapper">
              <div
                className={"ui dimmer " + (this.props.loading ? "active" : "")}
              >
                <div className="ui loader" />
              </div>
              {Object.keys(categoryBrands).map(catName => (
                <div className="products" id={catName}>
                  <div className="category-name">{catName}</div>
                  <div className="content">
                    {categoryBrands[catName].map(brand => (
                      <Brand brand={brand} />
                    ))}
                  </div>
                </div>
              ))}
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
    categoryBrands: rootCategoriesBrandsSelector(
      state,
      parseInt(props.match.params.id)
    ),
    category: categorySelector(state, parseInt(props.match.params.id)),
    loading: rootCategoriesBrandsLoadingSelector(state)
  };
};
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      RootCategoryBrandGet
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(SubCategoriesPage));
