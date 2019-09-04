import React, { Component } from "react";
import {
  productDescriptionSelector,
  productDescriptionLaodingSelector
} from "../../redux/selectors/product";
import NavBar from "../Reusable/navBar";
import SubNavBar from "../Reusable/subNavBar";
import Footer from "../Reusable/footer";
import SideBar from "../Reusable/sidebar";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { GetProductDescription } from "../../redux/products";
import { withLocalize } from "react-localize-redux";
import "../../assets/scss/individual.scss";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
import { Link } from "react-router-dom";
import { addToCart } from "../../redux/cart";
import AddToCart from "../Products/addToCart";
import { categorySelector } from "../../redux/selectors/category";
import { CategoryGet } from "../../redux/category";

class ProductPage extends Component {
  state = {
    quantity: 1
  };
  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "product-body";
    const language = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
    ).selected.id;
    const id = this.props.match.params.id;
    this.props.GetProductDescription(language, id);
    this.props.CategoryGet(language);
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

  quantityChanged(e) {
    const val = parseInt(e.target.value) ? parseInt(e.target.value) : 0;
    this.setState({ ...this.state, quantity: val });
  }
  render() {
    const { product, category } = this.props;
    let rightContent;
    const imageStyle = {
      backgroundImage: product.imagePath
        ? `url(${encodeURI(product.imagePath)})`
        : ""
    };
    if (product) {
      rightContent = (
        <div
          className={
            "product-wrapper " + this.props.loading ? "loading-wrapper" : ""
          }
        >
          <div className={"ui dimmer " + (this.props.loading ? "active" : "")}>
            <div className="ui loader" />
          </div>
          <div className="title">{product.brand}</div>
          <div className="product-content">
            <div className="image-part" style={imageStyle} />
            <div className="info">
              <h2 className="name">{product.name}</h2>
              <div>
                <p>
                  Volume: 0.75 l <br />
                  Color: red <br />
                  Type: dry <br />
                  Alcohol by volume: 13.5% <br />
                  Production year: 2015 <br />
                  Manufacturer: Highland Cellars LLC <br />
                  Produced in Armenia <br />
                  Code: 00130 <br />
                </p>
              </div>
              <div className="price">
                <span>€ {product.price}</span>
              </div>
              <div className="prop">
                <div className="product-count">
                  <button
                    className="button-count no-active"
                    onClick={this.minusButtonClicked}
                  >
                    -
                  </button>
                  <input
                    type="text"
                    onChange={this.quantityChanged.bind(this)}
                    className="number-product"
                    value={this.state.quantity}
                  />
                  <button
                    className="button-count"
                    onClick={this.plusButtonClicked}
                  >
                    +
                  </button>
                </div>
                <AddToCart
                  onClick={() =>
                    this.props.addToCart(product, this.state.quantity)
                  }
                />
              </div>
            </div>
          </div>

          <div className="information">
            <div className="description">
              <div className="title">
                <span>Description</span>
              </div>
              <div className="content">
                <span>{product.description}</span>
              </div>
            </div>
          </div>
        </div>
      );
    }

    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        <div className="content">
          <div className="breadcrumbs">
            <Link to="/">Home</Link>
            {category ? (
              <React.Fragment>
                <Link
                  to={
                    "/subcategory/" +
                    category.parentID +
                    "?scrollTo=" +
                    category.name
                  }
                >
                  {category.name}
                </Link>
                <Link
                  to={`/category/${category.id}/brand/${product.brandID}/${
                    product.brand
                  }`}
                >
                  {product.brand}
                </Link>
              </React.Fragment>
            ) : (
              ""
            )}
            <a>{product.name}</a>
          </div>
          <div className="body">{product ? rightContent : ""}</div>
        </div>

        <Footer />
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state, props) => {
  const product = productDescriptionSelector(state);
  let category;
  if (product) category = categorySelector(state, product.categoryID);

  return {
    product,
    loading: productDescriptionLaodingSelector(state),
    category
  };
};
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      GetProductDescription,
      addToCart,
      CategoryGet
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withLocalize(ProductPage));
