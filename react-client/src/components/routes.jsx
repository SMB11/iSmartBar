import React, { Component } from "react";
import { Route, Redirect } from "react-router-dom";
import LandingPage from "./Pages/landingPage";
import { languageStepStorageKey } from "./StartProcess/chooseLanguage";
import { locationStepStorageKey } from "./StartProcess/chooseLocation";
import { miniBarStepStorageKey } from "./StartProcess/chooseMiniBar";
import { withLocalize } from "react-localize-redux";
import SubCategoriesPage from "./Pages/subCategoriesPage";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { CategoryGet } from "../redux/category";

class Routes extends Component {
  componentWillUpdate() {
    if (window.sessionStorage.getItem(languageStepStorageKey)) {
      const language = JSON.parse(
        window.sessionStorage.getItem(languageStepStorageKey)
      ).selected.id;
      if (
        !this.props.activeLanguage ||
        this.props.activeLanguage.code !== language
      ) {
        this.props.setActiveLanguage(language);
      }

      this.props.CategoryGet(language);
      console.log("category get");
    }
  }
  render() {
    if (
      !JSON.parse(window.sessionStorage.getItem(languageStepStorageKey)) ||
      !JSON.parse(window.sessionStorage.getItem(locationStepStorageKey)) ||
      !JSON.parse(window.sessionStorage.getItem(miniBarStepStorageKey))
    )
      return <Redirect to="/process" />;
    else {
      return (
        <React.Fragment>
          <Route path="/" component={LandingPage} exact />
          <Route path="/subcategory/:id" component={SubCategoriesPage} exact />
        </React.Fragment>
      );
    }
  }
}

const mapStateToProps = (state, props) => props;
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
)(withLocalize(Routes));
