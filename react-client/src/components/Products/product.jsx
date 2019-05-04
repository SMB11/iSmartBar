import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { addToCart } from "../../redux/cart";
import { assetBaseUrl } from "../../api";
import { withRouter } from "react-router-dom";
import AddToCart from "./addToCart";
class Product extends Component {
  state = {
    quantity: 1
  };
  redirect(target) {
    this.props.history.push(target);
  }

  plusButtonClicked = () => {
    let oldState = { ...this.state };
    let newQuantity = oldState.quantity + 1;
    return this.setState({ oldState, quantity: newQuantity });
  };

  minusButtonClicked = () => {
    let oldState = { ...this.state };
    let newQuantity = oldState.quantity;
    if (oldState.quantity - 1 >= 1) {
      newQuantity = oldState.quantity - 1;
    }
    return this.setState({ oldState, quantity: newQuantity });
  };
  render() {
    const { product } = this.props;

    if (!product) return "";
    console.log(product);
    const imageStyle = {
      backgroundImage: product.imagePath
        ? `url(${encodeURI(assetBaseUrl + product.imagePath)})`
        : ""
    };
    console.log(product.imagePath);
    return (
      <div className="product">
        <div
          className="product-image"
          style={imageStyle}
          onClick={() => this.redirect("/product/" + product.id)}
        />
        <span className="product-title">{product.name}</span>
        <span className="price">€ {product.price}</span>
        <div className="prop">
          <div className="product-count">
            <button
              onClick={this.minusButtonClicked}
              className="button-count no-active"
            >
              -
            </button>
            <input
              type="text"
              readonly
              className="number-product"
              value={this.state.quantity}
            />
            <button onClick={this.plusButtonClicked} className="button-count">
              +
            </button>
          </div>
          <div className="button-content">
            <AddToCart
              onClick={() => this.props.addToCart(product, this.state.quantity)}
            />
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
)(withRouter(Product));
