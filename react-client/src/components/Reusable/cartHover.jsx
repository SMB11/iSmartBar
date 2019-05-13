import React, { Component } from "react";
import "../../assets/scss/cart.scss";
import MyCartItem from "./myCartItem";
import {
  insidePriceSelector,
  outisdePriceSelector,
  sectionCartSelector,
  sectionCartCountSelector
} from "../../redux/selectors/cart";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { withRouter } from "react-router";
import MyCartOtherItem from "./myCartOtherItem";

class CartHover extends Component {
  state = {
    smartBar: true,
    other: false,
    focused: 1
  };

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
  render() {
    const sectionOneCount = this.props.getSectionProductsCount(1);
    const prods = this.props.getSectionProducts(this.state.focused);
    const otherProds = this.props.otherProducts;
    return (
      <div className="cartHover">
        <div className="head">
          <div
            data-id="0"
            className={"ismartbar" + (this.state.smartBar ? " active" : "")}
          >
            <div
              onClick={this.smartBarButtonClicked.bind(this)}
              className="title"
            >
              <span>iSmartBar</span>
            </div>
          </div>
          <div
            data-id="1"
            className={"other" + (this.state.other ? " active" : "")}
          >
            <div onClick={this.otherButtonClicked.bind(this)} className="title">
              <span>Other</span>
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
                  (sectionOneCount === 5
                    ? "full"
                    : sectionOneCount > 0
                    ? "filled"
                    : "")
                }
                onClick={() => this.selectSection(1)}
              >
                {sectionOneCount + "/5"}
              </div>
              <div
                className="section section2"
                onClick={() => this.selectSection(2)}
              >
                {this.props.getSectionProductsCount(2) + "/5"}
              </div>
              <div
                className="section section3"
                onClick={() => this.selectSection(3)}
              >
                {this.props.getSectionProductsCount(3) + "/5"}
              </div>
              <div
                className="section section4"
                onClick={() => this.selectSection(4)}
              >
                {this.props.getSectionProductsCount(4) + "/5"}
              </div>
              <div
                className="section section5"
                onClick={() => this.selectSection(5)}
              >
                {this.props.getSectionProductsCount(5) + "/5"}
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
              : prods.map((p, index) => {
                  const res = [];
                  for (let i = 0; i < p.quantity; i++) {
                    res.push(<MyCartItem key={i} product={p} />);
                  }
                  return res;
                })}
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
      </div>
    );
  }
}

const mapStateToProps = (state, props) => {
  const insidePrice = insidePriceSelector(state);
  const outsidePrice = outisdePriceSelector(state);
  let total = (parseFloat(insidePrice) + parseFloat(outsidePrice)).toFixed(2);
  return {
    otherProducts: sectionCartSelector(state, 6),
    getSectionProducts: id => sectionCartSelector(state, id),
    getSectionProductsCount: id => sectionCartCountSelector(state, id),
    insidePrice,
    outsidePrice,
    total
  };
};
const mapDispatchToProps = dispatch => bindActionCreators({}, dispatch);

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(CartHover));
