import React, { Component } from "react";
import { Route, Redirect } from "react-router-dom";
import LandingPage from "./Pages/landingPage";
import { languageStepStorageKey } from "./StartProcess/chooseLanguage";
import { locationStepStorageKey } from "./StartProcess/chooseLocation";
import { miniBarStepStorageKey } from "./StartProcess/chooseMiniBar";
class Routes extends Component {
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

export default Routes;
