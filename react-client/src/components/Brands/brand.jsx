import React, { Component } from 'react';
import '../../assets/scss/products.scss'
import { assetBaseUrl } from '../../api';
import { Link } from "react-router-dom";
import { withRouter } from "react-router";
class Brand extends Component {

    redirect(target) {

        this.props.history.push(target);
    }
    render() {
        const { brand } = this.props;
        return (
            <div class="product" onClick={() => this.redirect.bind(this)("/brand/" + brand.id)}>
                <img src={assetBaseUrl + brand.imagePath} alt="" />
                <div class="background"></div>
                <span class="product-title">{brand.name}</span>
            </div>
        );
    }
}

export default withRouter(Brand);