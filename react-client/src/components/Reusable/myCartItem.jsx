import React, { Component } from "react";
import "../../assets/scss/mybar.scss";
import { assetBaseUrl } from "../../api";
import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { removeProduct } from "../../redux/cart";
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
        ? `url(${encodeURI(assetBaseUrl + product.imagePath)})`
        : ""
    };
    return (
      <React.Fragment>
        <div className="product" ref={ref => (this.prodRef = ref)}>
          <div className="prod-data">
            <Link to={"/product/" + product.id}>
              <div className="image" style={imageStyle} />
            </Link>
            <div className="name-container">
              <Link className="product-name" to={"/product/" + product.id}>
                {" "}
                {product.name}{" "}
              </Link>
              <p>€{product.price}</p>
            </div>
          </div>
          <div className="remove">
            <a
              onClick={() => this.props.removeProduct(product.id, product.size)}
            >
              Remove
            </a>
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
