import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { addToCart } from "../../redux/cart";
class Product extends Component {
  render() {
    const { id, name, price } = this.props.product;
    return (
      <div>
        <h1>{name}</h1>
        <p>{price}</p>
        <button onClick={() => this.props.addToCart(id)}>AddTocart</button>
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
)(Product);
