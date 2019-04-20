import React, { Component } from "react";
import { Route, Redirect } from "react-router-dom";
import LandingPage from "./Pages/landingPage";
import { languageStepStorageKey } from "./StartProcess/chooseLanguage";
import { locationStepStorageKey } from "./StartProcess/chooseLocation";
import { miniBarStepStorageKey } from "./StartProcess/chooseMiniBar";
import { withLocalize } from "react-localize-redux";
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
        </React.Fragment>
      );
    }
  }
}

export default withLocalize(Routes);
