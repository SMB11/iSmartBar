import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { addToCart } from "../../redux/cart";
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
        ? `url(${encodeURI(product.imagePath)})`
        : ""
    };
    return (
      <div
        className="product"
        onClick={() => this.redirect("/product/" + product.id)}
      >
        <div className="product-image" style={imageStyle} />
        <div className="info">
          <p>{product.category}</p>
          <div className="p-group">
            <p className="name">{product.name}</p>
            <p className="nowrap">{"€ " + product.price}</p>
          </div>
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
