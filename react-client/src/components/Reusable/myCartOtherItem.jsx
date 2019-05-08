import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import { assetBaseUrl } from "../../api";
import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { changeProductQuantity, removeProduct } from "../../redux/cart";
class myCartOtherItem extends Component {
  render() {
    const { product } = this.props;
    const imageStyle = {
      backgroundImage: product.imagePath
        ? `url(${encodeURI(assetBaseUrl + product.imagePath)})`
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
                      6,
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
                      6,
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
          <div className="prod-action">
            <div className="price">
              <span>€{product.price}</span>
            </div>
            <div className="remove">
              <button onClick={() => this.props.removeProduct(product.id, 6)}>
                Remove
              </button>
            </div>
          </div>
        </div>
        <div className="horizontal-line" />
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
)(myCartOtherItem);
