import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import { assetBaseUrl } from "../../api";
import { Link } from "react-router-dom";
class MyCartItem extends Component {
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
          <div className="prod-data">
            <div className="image" style={imageStyle} />
            <div>
              <Link className="product-name" to={"/product/" + product.id}>
                {" "}
                {product.name}{" "}
              </Link>
              <p>€{product.price}</p>
            </div>
          </div>
          <div className="remove">
            <button>Remove</button>
          </div>
        </div>
      </React.Fragment>
    );
  }
}
export default MyCartItem;
