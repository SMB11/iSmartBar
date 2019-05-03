import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import { assetBaseUrl } from "../../api";

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
            <div style={imageStyle} className="image" />
            <div>
              <p>{product.name}</p>
              <div className="product-count">
                <button className="button-count no-active" disabled>
                  -
                </button>
                <input
                  type="text"
                  readOnly
                  className="number-product"
                  value={product.quantity}
                />
                <button className="button-count">+</button>
              </div>
            </div>
          </div>
          <div className="prod-action">
            <div className="price">
              <span>€{product.price}</span>
            </div>
            <div className="remove">
              <button>Remove</button>
            </div>
          </div>
        </div>
        <div className="horizontal-line" />
      </React.Fragment>
    );
  }
}

export default myCartOtherItem;
