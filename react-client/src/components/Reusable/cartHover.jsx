import React, { Component } from "react";
import "../../assets/scss/cart.scss";
import MyCartItem from "./myCartItem";
import {
  insidePriceSelector,
  outisdePriceSelector,
  sectionCartSelector,
  sectionCartCountSelector,
  cartAllSelector
} from "../../redux/selectors/cart";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { withRouter } from "react-router";
import MyCartOtherItem from "./myCartOtherItem";
import { Link } from "react-router-dom";
import isMobile from "ismobilejs";
class CartHover extends Component {
  state = {
    smartBar: true,
    other: false,
    focused: 1
  };
  mainElement;
  otherButtonClicked = () => {
    this.setState({ ...this.state, other: true, smartBar: false });
  };
  smartBarButtonClicked = () => {
    this.setState({ ...this.state, other: false, smartBar: true });
  };
  selectSection = id => {
    if (id !== this.state.focused)
      this.setState({ ...this.state, focused: id });
  };
  handleViewCartClick = () => {
    const el = this.mainElement;
    window.focus();
  };
  render() {
    const {
      prods,
      isBarInsideProductsFilled,
      isBarInsideProductsFull,
      isBarLargeBottlesFilled,
      isBarLargeBottlesFull,
      barInsideProductsCount,
      barLargeBottlesCount
    } = this.props;
    const otherProds = this.props.otherProducts;
    return (
      <div
        className={"cartHover " + (isMobile.any ? "mobile" : "")}
        ref={ref => (this.mainElement = ref)}
      >
        <div className="head">
          <div
            data-id="0"
            className={"ismartbar" + (this.state.smartBar ? " active" : "")}
          >
            <div
              onClick={this.smartBarButtonClicked.bind(this)}
              className="title"
            >
              <span>Inside</span>
            </div>
          </div>
          <div
            data-id="1"
            className={"other" + (this.state.other ? " active" : "")}
          >
            <div onClick={this.otherButtonClicked.bind(this)} className="title">
              <span>Outside</span>
            </div>
          </div>
        </div>

        <div
          className={
            "myismart content-ismart  " + (this.state.smartBar ? " active" : "")
          }
        >
          <div className="minibarArea">
            <div className="minibar">
              <img src="http://localhost:3000/images/minibar.svg" />
              <div
                className={
                  "section section1 " +
                  (isBarInsideProductsFull
                    ? "full"
                    : isBarInsideProductsFilled
                    ? "filled"
                    : "")
                }
              >
                {barInsideProductsCount + "/20"}
              </div>
              <div
                className={
                  "section section2 " +
                  (isBarLargeBottlesFull
                    ? "full"
                    : isBarLargeBottlesFilled
                    ? "filled"
                    : "")
                }
              >
                {barLargeBottlesCount + "/5"}
              </div>
            </div>
          </div>
          <div className="separator" />
          <div
            className={
              "products " + (!prods || prods.length === 0 ? "empty" : "")
            }
          >
            {!prods || prods.length === 0
              ? "Empty"
              : prods.map((p, index) => <MyCartItem key={index} product={p} />)}
          </div>
        </div>

        <div
          className={
            "myismart content-other  " + (this.state.other ? " active" : "")
          }
        >
          <div
            className={
              "products " +
              (!otherProds || otherProds.length === 0 ? "empty" : "")
            }
          >
            {!otherProds || otherProds.length === 0
              ? "Empty"
              : otherProds.map((p, index) => {
                  return <MyCartOtherItem key={index} product={p} />;
                })}
          </div>
        </div>

        <div className="subtotal">
          <div className="text">Subtotal</div>
          <div className="value">֏ {this.props.total}</div>
        </div>

        <div className="viewCart">
          <Link
            className="button"
            onClick={this.handleViewCartClick.bind(this)}
            to="/cart"
          >
            View Cart
          </Link>
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state, props) => {
  const insidePrice = insidePriceSelector(state);
  const outsidePrice = outisdePriceSelector(state);
  let total = (parseFloat(insidePrice) + parseFloat(outsidePrice)).toFixed(2);
  const barInsideProductsCount = sectionCartCountSelector(state, 1);
  const barLargeBottlesCount = sectionCartCountSelector(state, 2);
  return {
    prods: cartAllSelector(state),
    otherProducts: sectionCartSelector(state, 3),
    isBarInsideProductsFilled: barInsideProductsCount !== 0,
    isBarInsideProductsFull: barInsideProductsCount === 20,
    barInsideProductsCount: barInsideProductsCount,
    isBarLargeBottlesFilled: barLargeBottlesCount !== 0,
    isBarLargeBottlesFull: barLargeBottlesCount === 5,
    barLargeBottlesCount: barLargeBottlesCount,
    getSectionProducts: id => sectionCartSelector(state, id),
    getSectionProductsCount: id => sectionCartCountSelector(state, id),
    total
  };
};
const mapDispatchToProps = dispatch => bindActionCreators({}, dispatch);

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(CartHover));
