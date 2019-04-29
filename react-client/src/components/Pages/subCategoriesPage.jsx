import React, { Component } from "react";
import "../../assets/scss/subcategory.scss";
import SubNavBar from "../Reusable/subNavBar";
import NavBar from "../Reusable/navBar";
import { rootCategoriesBrandsSelector } from "../../redux/selectors/category";
import { RootCategoryBrandGet } from "../../redux/category";

import { withLocalize, Translate } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import Brand from "../Brands/brand";
import SideBar from "../Reusable/sidebar";
import Footer from "../Reusable/footer";
class SubCategoriesPage extends Component {
  state = {};
  getBrands = () => {
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
    let { categoryBrands } = this.props;
    console.log(categoryBrands);

    if (!categoryBrands) categoryBrands = {};
    console.log(categoryBrands);
    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        <div class="content">
          <div class="breadcrumbs">
            <a href="">Category</a>
            <a href="">Subcategory</a>
          </div>
          <div class="body">
            <div class="left-content">
              <SideBar>
                <div class="filter">
                  <div class="title">Subcategory</div>
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
            <div class="right-content">
              {Object.keys(categoryBrands).map(catName => (
                <div class="products" id={catName}>
                  <div class="category-name">{catName}</div>
                  <div class="content">
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
    )
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
