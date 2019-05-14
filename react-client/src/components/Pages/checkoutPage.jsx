import React, { Component } from 'react';
import NavBar from '../Reusable/navBar';
import SubNavBar from '../Reusable/subNavBar';
import Footer from '../Reusable/footer';
import "../../assets/scss/checkout.scss";
import DropDown from '../Reusable/dropdown';


class CheckoutPage extends Component {
    render() {
        return (
            <React.Fragment>
                <NavBar />
                <SubNavBar />
                <div className="checkoutContent">
                    <div className="box">
                        <h1 className="purchaseTitle">Complete the Purchase</h1>
                        <div className="checkoutBody">
                            <div className="bodyContent">

                                <div className="checkoutSection">
                                    <h3 className="titleSection">Country</h3>
                                    <input className="selectCountry" disabled></input>
                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">First Name</h3>
                                    <input type="text" className="input" />
                                </div>

                                <div className="checkoutSection">
                                    <h3 className="titleSection">Hotel</h3>
                                    <input className="selectCountry" disabled />

                                </div>

                                <div className="checkoutSection">
                                    <h3 className="titleSection">Last Name</h3>
                                    <input type="text" className="input" />

                                </div>
                                <div className="checkoutSection">
                                    <h3 className="titleSection">City</h3>
                                    <input className="selectCountry" disabled />

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
                                    <span className="right">€ 12</span>
                                </div>
                                <div className="horizontalLine"></div>
                                <div className="outside">
                                    <span className="priceLabel">Items inside MiniBar:</span>
                                    <span className="right">€ 12</span>
                                </div>
                            </div>
                            <div className="checkoutPricesFinal">
                                <span className="finalPrice">TOTAL AMOUNT</span>
                                <span className="rightFinal">€ 12</span>
                            </div>
                            <div className="buttonDiv">

                                <button className="checkoutButton">CONFIRM PAYMENT<img  className="cc" src="http://localhost:3000/images/credit.svg" alt="cc" /> </button>
                            </div>

                        </div>
                    </div>
                </div>



                <Footer />

            </React.Fragment >
        );
    }
}

export default CheckoutPage;