import React, { Component } from 'react';
import '../../assets/scss/mybar.scss'
import { assetBaseUrl } from '../../api';

class myCartOtherItem extends Component {

    render() {
        const { product } = this.props;
        const imageStyle = {
            backgroundImage: product.imagePath
                ? `url(${encodeURI(assetBaseUrl + product.imagePath)})`
                : ""
        }
        return (
            <React.Fragment>
                <div class="product">
                    <div class="prod-info">
                        <div style={imageStyle} className="image"></div>
                        <div>
                            <p>{product.name}</p>
                            <div class="product-count">
                                <button class="button-count no-active" disabled>-</button>
                                <input type="text" readOnly class="number-product" value={product.quantity} />
                                <button class="button-count">+</button>
                            </div>
                        </div>
                    </div>
                    <div class="prod-action">
                        <div class="price">
                            <span>€{product.price}</span>
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