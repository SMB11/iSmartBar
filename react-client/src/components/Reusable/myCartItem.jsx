import React, { Component } from 'react';
import '../../assets/scss/mybar.scss';

class MyCartItem extends Component {

    render() {
        return (
            <React.Fragment>
                <div class="product">
                    <div class="prod-data">
                        <img src="images/products/product-small.svg" alt="" />
                        <div>
                            <p class="wine">Wine</p>
                            <p>KOOR</p>
                            <p>$29.99</p>
                        </div>
                    </div>
                    <div class="remove">
                        <button>Remove</button>
                    </div>
                </div>
                <div class="horizontal-line"></div>
                <div class="horizontal-line"></div>
                <div class="horizontal-line"></div>
            </React.Fragment>
        );
    }
}

export default MyCartItem;