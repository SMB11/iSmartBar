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
    scrollTo: undefined,
    scrolled: false
  };
  catRefs = {};
  getParameterByName = (name, url) => {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
      results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return "";
    return decodeURIComponent(results[2].replace(/\+/g, " "));
  };
  getBrands = () => {
    const oldScrollTo = this.state.scrollTo;
    const newState = { ...this.state };
    newState.scrollTo = this.getParameterByName(
      "scrollTo",
      window.location.href
    );
    if (newState.scrollTo !== oldScrollTo) {
      newState.scrolled = false;
      this.setState(newState);
    }

    const language = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
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
    if (
      this.props.categoryBrands &&
      !this.state.scrolled &&
      this.state.scrollTo
    ) {
      console.log(this.catRefs);
      if (this.catRefs[this.state.scrollTo]) {
        console.log(this.catRefs[this.state.scrollTo].offsetTop);
        window.scrollTo(0, this.catRefs[this.state.scrollTo].offsetTop);
      }
      this.setState({ ...this.state, scrolled: true });
    }
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
                      <p key={catName}>
                        <a href={"#" + catName}>{catName}</a>
                      </p>
                    ))}
                  </div>
                </div>
              </SideBar>
            </div>
            <div
              className={
                "right-content  " +
                (this.props.loading ? "loading-wrapper" : "")
              }
            >
              <div
                className={"ui dimmer " + (this.props.loading ? "active" : "")}
              >
                <div className="ui loader" />
              </div>
              {Object.keys(categoryBrands).map(catName => (
                <div
                  className="products"
                  ref={ref => (this.catRefs[catName] = ref)}
                  key={catName}
                  id={catName}
                >
                  <div className="category-name">{catName}</div>
                  <div className="content">
                    {categoryBrands[catName].map(brand => (
                      <Brand key={brand.id} brand={brand} />
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
