import React, { Component } from "react";
import NavBar from "../Reusable/navBar";
import SubNavBar from "../Reusable/subNavBar";
import "../../assets/scss/mybar.scss";
import Section from "../Reusable/section";
import MyCartOtherItem from "../Reusable/myCartOtherItem";
import SideBar from "../Reusable/sidebar";
import Footer from "../Reusable/footer";
import { connect } from "react-redux";
import {
  sectionCartSelector,
  insidePriceSelector,
  outisdePriceSelector
} from "../../redux/selectors/cart";
import { OutgoingMessage } from "http";
import { withRouter } from "react-router";
import { Link } from "react-router-dom";
import { CategoryGet } from "../../redux/category";
import { bindActionCreators } from "redux";
import { languageStepStorageKey } from "../StartProcess/chooseLanguage";
class CartPage extends Component {
  state = {
    smartBar: true,
    other: false
  };

  componentDidMount() {
    document.getElementsByTagName("body")[0].className = "cart-body";
    this.apiCalls();
  }

  apiCalls() {
    const language = JSON.parse(
      window.localStorage.getItem(languageStepStorageKey)
    ).selected.id;
    this.props.CategoryGet(language);
  }

  otherButtonClicked = () => {
    this.setState({ ...this.state, other: true, smartBar: false });
  };
  smartBarButtonClicked = () => {
    this.setState({ ...this.state, other: false, smartBar: true });
  };
  render() {
    const prod = this.props.products;
    return (
      <React.Fragment>
        <NavBar />
        <SubNavBar />
        <div className="content">
          <h1 className="title">My iSmartBar</h1>
          <div className="body">
            <div className="left-content">
              <div className="head">
                <div
                  data-id="0"
                  className={
                    "ismartbar" + (this.state.smartBar ? " active" : "")
                  }
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
                  <div
                    onClick={this.otherButtonClicked.bind(this)}
                    className="title"
                  >
                    <span>Other</span>
                  </div>
                </div>
              </div>
              <div
                className={
                  "myismart content-ismart  " +
                  (this.state.smartBar ? " active" : "")
                }
              >
                <Section sectionTitle="Section 1" size={1} />
                <Section sectionTitle="Section 2" />
                <Section sectionTitle="Section 3" />
                <Section sectionTitle="Section 4" />
                <Section sectionTitle="Section 5" />
              </div>

              <div
                className={
                  "myismart content-other" + (this.state.other ? " active" : "")
                }
              >
                <h2 className="title">Other</h2>

                {prod.map((p, index) => {
                  return <MyCartOtherItem key={index} product={p} />;
                })}
              </div>
            </div>
            <div className="right-content">
              <div className="summary">
                <span className="title">Order Summary</span>
                <div className="horizontal-line" />
                <div>
                  <span>Items inside MiniBar:</span>
                  <span className="right">€ {this.props.insidePrice}</span>
                </div>
                <div>
                  <span>Items outisde MiniBar:</span>
                  <span className="right">€ {this.props.outsidePrice}</span>
                </div>
                <div className="horizontal-line" />
                <div>
                  <span>Order total:</span>
                  <span className="right">€ {this.props.total}</span>
                </div>
                <button>CHECKOUT</button>
                <div className="go-back">
                  <Link to="/#category">Continue Shopping</Link>
                </div>
              </div>
            </div>
          </div>
        </div>

        <Footer />
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state, props) => {
  const insidePrice = insidePriceSelector(state);
  const outsidePrice = outisdePriceSelector(state);
  let total = (parseFloat(insidePrice) + parseFloat(outsidePrice)).toFixed(2);
  return {
    products: sectionCartSelector(state, 6),
    insidePrice,
    outsidePrice,
    total
  };
};
const mapDispatchToProps = dispatch =>
  bindActionCreators(
    {
      CategoryGet
    },
    dispatch
  );

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(CartPage));
