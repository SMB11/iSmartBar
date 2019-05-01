import React, { Component } from 'react';
import NavBar from '../Reusable/navBar';
import SubNavBar from '../Reusable/subNavBar';
import '../../assets/scss/mybar.scss'
import Section from '../Reusable/section';
import MyCartOtherItem from '../Reusable/myCartOtherItem';
import SideBar from '../Reusable/sidebar';
import Footer from '../Reusable/footer';
class CartPage extends Component {
    state = {
        smartBar: true,
        other: false
    };

    componentDidMount() {
        document.getElementsByTagName("body")[0].className = "cart-body";
    }

    otherButtonClicked = () => {
        this.setState({ ...this.state, other: true, smartBar: false });
    }
    smartBarButtonClicked = () => {
        this.setState({ ...this.state, other: false, smartBar: true });
    }
    render() {
        return (
            <React.Fragment>
                <NavBar />
                <SubNavBar />
                <div class="content">
                    <h1 class="title">My iSmartBar</h1>
                    <div class="body">
                        <div class="left-content">
                            <div class="head">
                                <div data-id="0" className={"ismartbar" + (this.state.smartBar ? " active" : "")}>
                                    <div onClick={this.smartBarButtonClicked.bind(this)} class="title">
                                        <span>iSmartBar</span>
                                    </div>
                                </div>
                                <div data-id="1" className={"other" + (this.state.other ? " active" : "")}>
                                    <div onClick={this.otherButtonClicked.bind(this)} class="title">
                                        <span >Other</span>
                                    </div>
                                </div>
                            </div>
                            <div className={"myismart content-ismart  " + (this.state.smartBar ? " active" : "")}>
                                <Section sectionTitle="Section 1" />
                                <Section sectionTitle="Section 2" />
                                <Section sectionTitle="Section 3" />
                                <Section sectionTitle="Section 4" />
                                <Section sectionTitle="Section 5" />
                            </div>

                            <div className={"myismart content-other" + (this.state.other ? " active" : "")}>

                                <h2 class="title">Other</h2>
                                <MyCartOtherItem />
                                <MyCartOtherItem />
                                <MyCartOtherItem />
                                <MyCartOtherItem />
                            </div>


                        </div>
                        <div class="right-content">
                            <div class="summary">
                                <span class="title">Order Summary</span>
                                <div class="horizontal-line"></div>
                                <div>
                                    <span>Items:</span>
                                    <span class="right">$99.99</span>
                                </div>
                                <div>
                                    <span>Shipping:</span>
                                    <span class="right">$0.00</span>
                                </div>
                                <div>
                                    <span>Total before tax:</span>
                                    <span class="right">$99.99</span>
                                </div>
                                <div>
                                    <span>Estimated tax to be collected:*</span>
                                    <span class="right">$0.00</span>
                                </div>
                                <div class="horizontal-line"></div>
                                <div>
                                    <span>Order total:</span>
                                    <span class="right">$99.99</span>
                                </div>
                                <button>CHECKOUT</button>
                            </div>
                        </div>
                    </div>
                </div>
                <Footer />
            </React.Fragment >
        );
    }
}

export default CartPage;