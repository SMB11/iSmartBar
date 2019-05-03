import React, { Component } from "react";
import "../../assets/scss/products.scss";
import { assetBaseUrl } from "../../api";
import { Link } from "react-router-dom";
import { withRouter } from "react-router";
class Brand extends Component {
  redirect(target) {
    this.props.history.push(target);
  }
  render() {
    const { brand } = this.props;
    return (
      <div
        className="product"
        onClick={() =>
          this.redirect.bind(this)("/brand/" + brand.id + "/" + brand.name)
        }
      >
        <img src={assetBaseUrl + brand.imagePath} alt="" />
        <div className="background" />
        <span className="product-title">{brand.name}</span>
      </div>
    );
  }
}

export default withRouter(Brand);
