import React, { Component } from 'react';
import NavBar from '../Reusable/navBar';
import SubNavBar from '../Reusable/subNavBar';
import Footer from '../Reusable/footer';
import "../../assets/scss/checkout.scss";
import DropDown from '../Reusable/dropdown';
import { connect } from "react-redux";
import {
  sectionCartSelector,
  insidePriceSelector,
  outisdePriceSelector
} from "../../redux/selectors/cart";
import { OutgoingMessage } from "http";
import { bindActionCreators } from "redux";
import { withRouter } from "react-router";
import { locationStepStorageKey } from "../StartProcess/chooseLocation";

class CheckoutPage extends Component {
    state = {
    hotel: {},
    city: {},
    country: {}
    };
    componentDidMount(){
        this.setState({...this.state, ...JSON.parse(window.localStorage.getItem(locationStepStorageKey))});
    }
    render() {
        return (
            <React.Fragment>
                <NavBar showSearch={false}/>
                <div className="checkoutContent">
                    <div className="box">
                        <h1 className="purchaseTitle">Complete the Purchase</h1>
                        <div className="checkoutBody">
                            <div className="bodyContent">

                                <div className="checkoutSection">
                                    <h3 className="titleSection">Country</h3>
                                    <input className="selectCountry" disabled value={this.state.country.name}></input>
                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">City</h3>
                                    <input className="selectCountry" disabled value={this.state.city.name}/>

                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">Hotel</h3>
                                    <input className="selectCountry" disabled value={this.state.hotel.name}/>

                                </div>
                                <div className="checkoutSection">
                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">First Name</h3>
                                    <input type="text" className="input" />
                                </div>


                                <div className="checkoutSection">
                                    <h3 className="titleSection">Last Name</h3>
                                    <input type="text" className="input" />

                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">Email *</h3>
                                    <input type="email" className="input" />

                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">Phone *</h3>
                                    <input type="text" className="input" />

                                </div>



                            </div>
                            <div className="checkoutPrices">
                                <div className="inside">
                                    <span className="priceLabel">Items inside MiniBar:</span>
                                    <span className="right">֏ {this.props.insidePrice}</span>
                                </div>
                                <div className="horizontalLine"></div>
                                <div className="outside">
                                    <span className="priceLabel">Items inside MiniBar:</span>
                                    <span className="right">֏ {this.props.outsidePrice}</span>
                                </div>
                            </div>
                            <div className="checkoutPricesFinal">
                                <span className="finalPrice">TOTAL AMOUNT</span>
                                <span className="rightFinal">֏ {this.props.total}</span>
                            </div>
                            <div className="buttonDiv">

                                <button className="checkoutButton">CONTINUE TO PAYMENT<img  className="cc" src="http://localhost:3000/images/credit.svg" alt="cc" /> </button>
                            </div>

                        </div>
                    </div>
                </div>




            </React.Fragment >
        );
    }
}

const mapStateToProps = (state, props) => {
    const insidePrice = insidePriceSelector(state);
    const outsidePrice = outisdePriceSelector(state);
    let total = (parseFloat(insidePrice) + parseFloat(outsidePrice)).toFixed(2);
    return {
      insidePrice,
      outsidePrice,
      total
    };
  };
  const mapDispatchToProps = dispatch =>
    bindActionCreators(
      {
      },
      dispatch
    );
  
  export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(withRouter(CheckoutPage));