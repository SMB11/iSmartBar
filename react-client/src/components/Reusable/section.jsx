import React, { Component } from 'react';
import '../../assets/scss/mybar.scss'
import MyCartItem from './myCartItem';

class Section extends Component {

    render() {
        return (
            <div class="section">
                <h3 class="title">{this.props.sectionTitle}</h3>
                <div class="section-body">
                    <MyCartItem />
                </div>
            </div>
        );
    }
}

export default Section;