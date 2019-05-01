import React, { Component } from 'react';
import '../../assets/scss/mybar.scss'

class myCartOtherItem extends Component {

    render() {
        return (
            <React.Fragment>
                <div class="product">
                    <div class="prod-info">
                        <img src="images/products/product-middle.svg" alt="" />
                        <div>
                            <p class="wine">Wine</p>
                            <p>KOOR</p>
                            <div class="product-count">
                                <button class="button-count no-active" disabled>-</button>
                                <input type="text" readonly class="number-product" value="1" />
                                <button class="button-count">+</button>
                            </div>
                        </div>
                    </div>
                    <div class="prod-action">
                        <div class="price">
                            <span>$29.99</span>
                        </div>
                        <div class="remove">
                            <button>Remove</button>
                        </div>
                    </div>
                </div>
                <div class="horizontal-line"></div>
            </React.Fragment>
        );
    }
}

export default myCartOtherItem;