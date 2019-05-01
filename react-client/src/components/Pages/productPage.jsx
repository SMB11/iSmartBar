import React, { Component } from 'react';
import { productDescriptionSelector } from '../../redux/selectors/product';
import NavBar from '../Reusable/navBar';
import SubNavBar from '../Reusable/subNavBar';
import Footer from '../Reusable/footer';
import SideBar from '../Reusable/sidebar';
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { GetProductDescription } from '../../redux/products';
import { withLocalize } from 'react-localize-redux'
import '../../assets/scss/individual.scss';
import { languageStepStorageKey } from '../StartProcess/chooseLanguage';
import { assetBaseUrl } from '../../api';

class ProductPage extends Component {

    componentDidMount() {

        document.getElementsByTagName("body")[0].className = "product-body";
        const language = JSON.parse(
            window.sessionStorage.getItem(languageStepStorageKey)
        ).selected.id;
        const id = this.props.match.params.id;
        this.props.GetProductDescription(language, id);

    }
    render() {
        const { product } = this.props;
        console.log(product);
        let rightContent;
        if (product) {
            rightContent = (
                <div class="right-content">
                    <div class="title">{product.name}</div>
                    <div class="product-content">
                        <div class="image-part">
                            <img src={product.imagePath ? assetBaseUrl + product.imagePath : null} alt="" />
                        </div>
                        <div class="info">
                            <h2 class="name"></h2>
                            <div class="rate">
                                <form action="" method="post" class="ratized">
                                    <input id="radio1" type="radio" name="estrellas" value="1" />
                                    <label for="radio1">&#9733;</label>

                                    <input id="radio2" type="radio" name="estrellas" value="2" />
                                    <label for="radio2">&#9733;</label>

                                    <input id="radio3" type="radio" name="estrellas" value="3" />
                                    <label for="radio3">&#9733;</label>

                                    <input id="radio4" type="radio" name="estrellas" value="4" />
                                    <label for="radio4">&#9733;</label>

                                    <input id="radio5" type="radio" name="estrellas" value="5" />
                                    <label for="radio5">&#9733;</label>

                                </form>
                            </div>
                            <div>
                                <p>Volume: 0.75 l</p>
                                <p>Color: red </p>
                                <p>Type: dry</p>
                                <p>Alcohol by volume: 13.5%</p>
                                <p>Production year: 2015</p>
                                <p>Manufacturer: Highland Cellars LLC </p>
                                <p>Produced in Armenia</p>
                                <p>Code: 00130</p>
                            </div>
                            <div class="price">
                                <span>€ {product.price}</span>
                            </div>
                            <div class="prop">
                                <div class="product-count">
                                    <button class="button-count no-active" disabled>-</button>
                                    <input type="text" readonly class="number-product" value="1" />
                                    <button class="button-count">+</button>
                                </div>
                                <div class="button-content">
                                    <button class="btn">
                                        <img src="images/add-to-cart.svg" alt="" />
                                        <span>Add to Cart</span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="information">
                        <div class="description">
                            <div class="title">
                                <span>Description</span>
                            </div>
                            <div class="content">
                                <span>
                                    {product.description}
                                </span>
                            </div>
                        </div>
                    </div>


                </div>
            );
        }
        return (
            <React.Fragment>
                <NavBar />
                <SubNavBar />
                <div class="content">
                    <div class="breadcrumbs">
                        <a href="">Category</a>
                        <a href="">Subcategory</a>
                        <a href="">Brand</a>
                    </div>
                    <div class="body">
                        <div class="left-content">
                            <SideBar />
                        </div>
                        {product ? rightContent : ""}
                    </div>
                </div>
                <Footer />
            </React.Fragment>
        );
    }
}


const mapStateToProps = (state, props) => {
    return {
        product: productDescriptionSelector(
            state
        )
    };
};
const mapDispatchToProps = dispatch =>
    bindActionCreators(
        {
            GetProductDescription
        },
        dispatch
    );

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withLocalize(ProductPage));