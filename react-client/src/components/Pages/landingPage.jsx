import React, { Component } from "react";
import NavBar from "../Reusable/navBar";
import Product from "../Products/product";
import api from "../../api";
import { withLocalize } from "react-localize-redux";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { CategoryGet } from "../../redux/category";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import "../../assets/scss/home.scss";
class landingPage extends Component {
  state = {};
  componentDidMount() {
    console.log(this.props);
    // api.categories.getAll(this.props.activeLanguage.code).then(data => {
    //   console.log(data);
    // });
    const language = JSON.parse(
      window.sessionStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.CategoryGet(language);
  }

  render() {
    return (
      <div id="container">
        <NavBar />
        {/* <Product
          product={{
            id: 1,
            name: "vardanik",
            price: 1
          }}
        /> */}
      </div>
    );
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
)(withLocalize(landingPage));
