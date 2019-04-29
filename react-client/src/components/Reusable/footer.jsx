import React, { Component } from 'react';
import '../../assets/scss/footer.scss'
class Footer extends Component {

    render() {
        return (<footer>
            <div class="footer-content">
                <div class="footer-menu">
                    <ul>
                        <li>
                            <a href="#">Home</a>
                        </li>
                        <li>
                            <a href="#">About</a>
                        </li>
                        <li>
                            <a href="#">Categories</a>
                        </li>
                        <li>
                            <a href="#">iSmartBar</a>
                        </li>
                        <li>
                            <a href="#">Contact</a>
                        </li>
                    </ul>
                </div>
                <div class="footer-line" />
                <div class="social-icons">
                    <a href="#">
                        <img src="http://localhost:3000/images/gmail.svg" alt="gmail" />
                    </a>
                    <a href="#">
                        <img src="http://localhost:3000/images/ln.svg" alt="ln" />
                    </a>
                    <a href="#">
                        <img src="http://localhost:3000/images/twitter.svg" alt="twitter" />
                    </a>
                    <a href="#">
                        <img src="http://localhost:3000/images/fb.svg" alt="fb" />
                    </a>
                    <a href="#">
                        <img src="http://localhost:3000/images/instagram.svg" alt="instagram" />
                    </a>
                </div>
                <div class="footer-info">
                    <a href="mailto:info@companyname.com">
                        Email: info@companyname.com
                </a>
                    <a href="tel:+37400000000">Phone: (374) xxx xxxx</a>
                    <a href="">Address: Biagio Capone, Via Castagneto 2, Italy</a>
                </div>
                <div class="short-line" />
                <div class="copyright">
                    <span>2019. iSmartBar. All rights reserved</span>
                </div>
            </div>
        </footer>);
    }
}

export default Footer;