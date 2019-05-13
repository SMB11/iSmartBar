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
                <div className="content">
                    <h1 className="title">Complete the Purchase</h1>
                    <div className="body">
                        <div className="bodyContent">
                            <div className="section">
                                <h3 className="titleSection">Country</h3>
                                <select className="selectCountry"></select>
                            </div>
                            <div className="section">
                                <h3 className="titleSection">City</h3>
                                <select className="selectCountry"></select>

                            </div>
                            <div className="section">
                                <h3 className="titleSection">Hotel</h3>
                                <select className="selectCountry"></select>

                            </div>
                            <div className="section">
                                <h3 className="titleSection">First Name</h3>
                                <input type="text" className="input" />
                            </div>
                            <div className="section">
                                <h3 className="titleSection">Last Name</h3>
                                <input type="text" className="input" />

                            </div>
                            <div className="section">
                                <h3 className="titleSection">Email *</h3>
                                <input type="text" className="input" />

                            </div>
                            <div className="section">
                                <h3 className="titleSection">Phone *</h3>
                                <input type="text" className="input" />

                            </div>
                        </div>

                    </div>

                </div>

                <Footer />

            </React.Fragment>
        );
    }
}

export default CheckoutPage;