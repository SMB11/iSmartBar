import React, { Component } from "react";
import "../../assets/scss/footer.scss";
class Footer extends Component {
  render() {
    return (
      <footer>
        <div className="footer-content">
          <div className="social-icons">
            <a href="#">
              <img src="http://localhost:3000/images/gmail.svg" alt="gmail" />
            </a>
            <a href="#">
              <img src="http://localhost:3000/images/ln.svg" alt="ln" />
            </a>
            <a href="#">
              <img
                src="http://localhost:3000/images/twitter.svg"
                alt="twitter"
              />
            </a>
            <a href="#">
              <img src="http://localhost:3000/images/fb.svg" alt="fb" />
            </a>
            <a href="#">
              <img
                src="http://localhost:3000/images/instagram.svg"
                alt="instagram"
              />
            </a>
          </div>
          <div className="footer-info">
            <a href="mailto:info@companyname.com">
              Email: info@companyname.com
            </a>
            <a href="tel:+37400000000">Phone: (374) xxx xxxx</a>
            <a href="">Address: Biagio Capone, Via Castagneto 2, Italy</a>
          </div>
          <div className="short-line" />
          <div className="copyright">
            <span>2019. iSmartBar. All rights reserved</span>
          </div>
        </div>
      </footer>
    );
  }
}

export default Footer;
