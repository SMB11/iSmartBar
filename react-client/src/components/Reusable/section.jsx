import React, { Component } from 'react';
import '../../assets/scss/mybar.scss'
import MyCartItem from './myCartItem';
import { sectionCartSelector } from '../../redux/selectors/cart'
import { connect } from 'react-redux'
import { read } from 'fs';
class Section extends Component {

    render() {
        console.log("size: " + this.props.size);
        const prod = this.props.products;
        return (
            <div class="section">
                <h3 class="title">{this.props.sectionTitle}</h3>
                <div class="section-body">
                    {(!prod || prod.length === 0) ? "Empty" :
                        prod.map((p, index) => {
                            const res = [];
                            for (let i = 0; i < p.quantity; i++) {
                                res.push(<MyCartItem key={i} product={p} />)
                            }
                            return res;
                        })}
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state, props) => {
    return {
        products: sectionCartSelector(
            state,
            props.size
        ),
    };
};


export default connect(mapStateToProps)(Section);