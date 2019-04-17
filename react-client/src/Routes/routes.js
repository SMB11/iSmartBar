import React, { Component } from "react";
import { Route } from "react-router-dom";
import StartProcessPage from "../components/StartProcess/startProcessPage";
import LandingPage from "../components/LandingPage/landingPage";

class routes extends Component {
  render() {
    return (
      <div>
        <Route exact path="/" component={StartProcessPage} />
        <Route exact path="/custom" component={LandingPage} />
      </div>
    );
  }
}

export default routes;
