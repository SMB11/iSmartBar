import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { addToCart } from "../../redux/cart";
import { assetBaseUrl } from "../../api";
import { withRouter } from "react-router-dom";
class Product extends Component {
  redirect(target) {
    this.props.history.push(target);
  }
  render() {
    const { product } = this.props;

    if (!product) return "";
    console.log(product);
    return (
      <div class="product">
        <img onClick={() => this.redirect("/product/" + product.id)} src={product.imagePath ? product.imagePath : ""} alt="" />
        <span class="product-title">{product.name}</span>
        <span class="price">{product.price}</span>
        <div class="prop">
          <div class="product-count">
            <button class="button-count no-active" disabled>-</button>
            <input type="text" readonly class="number-product" value="1" />
            <button class="button-count">+</button>
          </div>
          <div class="button-content">
            <button onClick={() => this.props.addToCart(product.id)} class="btn">
              <img src="http://localhost:3000/images/add-to-cart.svg" alt="" />
              <span>Add to Cart</span>
            </button>
          </div>
        </div>
      </div >

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
