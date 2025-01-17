import React, { Component } from "react";
import "../../assets/scss/products.scss";
import { Link } from "react-router-dom";
import { withRouter } from "react-router";
class Brand extends Component {
  render() {
    const { brand } = this.props;
    return (
      <div
        ref={this.props.refProp}
        className="product"
        onClick={this.props.onClick}
      >
        <img src={brand.imagePath} alt="" />
        <div className="background" />
        <span className="product-title">{brand.name}</span>
      </div>
    );
  }
}

export default withRouter(Brand);
