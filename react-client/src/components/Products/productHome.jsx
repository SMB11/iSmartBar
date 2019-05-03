import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { addToCart } from "../../redux/cart";
import { assetBaseUrl } from "../../api";
import { withRouter } from "react-router-dom";
class ProductHome extends Component {
  redirect(target) {
    this.props.history.push(target);
  }

  redirect(target) {
    this.props.history.push(target);
  }
  render() {
    const { product } = this.props;

    if (!product) return "";
    const imageStyle = {
      backgroundImage: product.imagePath
        ? `url(${encodeURI(assetBaseUrl + product.imagePath)})`
        : ""
    };
    return (
      <div
        className="product"
        onClick={() => this.redirect("/product/" + product.id)}
      >
        <div className="product-image">
          <img src={assetBaseUrl + product.imagePath} alt="" />
        </div>
        <div className="info">
          <p>{product.category}</p>
          <p>{product.name}</p>
          <p>€ {product.price}</p>
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state, props) => props;

const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      addToCart
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(ProductHome));
