import React, { Component } from "react";
import SubNavBar from "../Reusable/subNavBar";
import NavBar from "../Reusable/navBar";

class SubCategoriesPage extends Component {
  state = {};
  render() {
    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        {this.props.match.params.id}
      </React.Fragment>
    );
  }
}

export default SubCategoriesPage;
