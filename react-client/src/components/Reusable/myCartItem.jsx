import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import { assetBaseUrl } from "../../api";
import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { removeProduct } from '../../redux/cart'
import { connect } from 'react-redux'
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
            <button onClick={() => this.props.removeProduct(product.id, product.size)}>Remove</button>
          </div>
        </div>
      </React.Fragment>
    );
  }
}


const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      removeProduct
    },
    dispatch
  );

export default connect(
  null,
  mapDispatchToProps
)(MyCartItem);
