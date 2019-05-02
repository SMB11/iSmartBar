import React, { Component } from 'react';
import '../../assets/scss/mybar.scss';
import { assetBaseUrl } from '../../api';

class MyCartItem extends Component {

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
                    <div class="prod-data">
                        <div className="image" style={imageStyle}></div>
                        <div>
                            <p>{product.name}</p>
                            <p>€{product.price}</p>
                        </div>
                    </div>
                    <div class="remove">
                        <button>Remove</button>
                    </div>
                </div>

            </React.Fragment>
        );
    }
}

export default MyCartItem;