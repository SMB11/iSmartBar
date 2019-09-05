import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { removeProduct, changeProductQuantity } from "../../redux/cart";
import { connect } from "react-redux";
class MyCartItem extends Component {
  prodRef;
  componentDidUpdate(prevProps) {
    // if (prevProps.product.id !== this.props.product.id) {
    //   this.prodRef.classList.add("animate");
    //   setTimeout(() => {
    //     this.prodRef.classList.remove("animate");
    //   }, 50);
    // }
  }
  render() {
    const { product } = this.props;
    const imageStyle = {
      backgroundImage: product.imagePath
        ? `url(${encodeURI(product.imagePath)})`
        : ""
    };
    return (
      <React.Fragment>
        <div className="product">
          <div className="prod-info">
            <Link to={"/product/" + product.id}>
              <div style={imageStyle} className="image" />
            </Link>
            <div>
              <Link className="product-name" to={"/product/" + product.id}>
                {product.name}
              </Link>
              <div className="product-count">
                <button
                  onClick={() =>
                    this.props.changeProductQuantity(
                      product.id,
                      product.size,
                      Math.max(product.quantity - 1, 1)
                    )
                  }
                  className="button-count no-active"
                >
                  -
                </button>
                <input
                  type="text"
                  readOnly
                  className="number-product"
                  value={product.quantity}
                />
                <button
                  onClick={() =>
                    this.props.changeProductQuantity(
                      product.id,
                      product.size,
                      product.quantity + 1
                    )
                  }
                  className="button-count"
                >
                  +
                </button>
              </div>
            </div>
          </div>
          <div className="price">֏{product.price}</div>

          <div
            className="remove"
            onClick={() =>
              this.props.removeProduct(product.id, product.size, true)
            }
          >
            <img src="http://localhost:3000/images/close.svg" />
          </div>
        </div>
      </React.Fragment>
    );
  }
}

const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      changeProductQuantity,
      removeProduct
    },
    dispatch
  );

export default connect(
  null,
  mapDispatchToProps
)(MyCartItem);
