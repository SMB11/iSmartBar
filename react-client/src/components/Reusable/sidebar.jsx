import React, { Component } from 'react';
import '../../assets/scss/products.scss';
class SideBar extends Component {

    render() {
        return (
            <div class="sidebar">
                {this.props.children}
            </div>
        );
    }
}

export default SideBar;