import React from 'react';
import DesktopHeader from '../DesktopHeader/index';
import './Payment.css';

const PaymentPage = () => {
  return (
    <div className="container-fluid px-1 px-md-2 px-lg-4 py-5 mx-auto">
        <DesktopHeader/>
      <div className="row d-flex justify-content-center">
        <div className="col-xl-7 col-lg-8 col-md-9 col-sm-11">
          <div className="card border-0">
            <div className="row justify-content-center">
              <h3 className="mb-4">Credit Card Checkout</h3>
            </div>
            <div className="row">
              <div className="col-sm-7 border-line pb-3">
                <div className="form-group">
                  <p className="text-muted text-sm mb-0">Name on the card</p>
                  <input type="text" name="name" placeholder="Name" size="15" />
                </div>
                <div className="form-group">
                  <p className="text-muted text-sm mb-0">Card Number</p>
                  <div >
                    <input type="text" name="card-num" placeholder="0000 0000 0000 0000" size="18" id="cr_no" minLength="19" maxLength="19" />
                   
                    
                  </div>
                </div>
                <div className="form-group">
                  <p className="text-muted text-sm mb-0">Expiry date</p>
                  <input type="text" name="exp" placeholder="MM/YY" size="6" id="exp" minLength="5" maxLength="5" />
                </div>
               
                
              </div>
              <div className="col-sm-5 text-sm-center justify-content-center pt-4 pb-4">
                
                <small className="text-sm text-muted">Payment amount</small>
                <div className="row px-3 justify-content-sm-center">
                  <h2>
                    <span className="text-md font-weight-bold mr-2">$</span>
                    <span className="text-danger">59.49</span>
                  </h2>
                </div>
                <button type="submit" className="btn btn-red text-center mt-4">PAY</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PaymentPage;
