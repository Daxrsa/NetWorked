import React, { useState } from 'react';
//import { IPaymentDto } from '../Interfaces/payment';
import DesktopHeader from '../DesktopHeader/index';
import './Payment.css';
import { useNavigate } from 'react-router-dom';
import httpUserUrl from '../../Helpers/http.user.module';
import axios from 'axios';
import { logout } from '../AuthService';

const PaymentPage = () =>
{
  const [customerId, setCustomerId] = useState("");
  const redirect = useNavigate();
  const token = localStorage.getItem("jwtToken");

  const handleSubmit = async (event) =>
  {
    event.preventDefault();

    console.log("working");
    await axios.post(
      "http://localhost:5116/api/Stripe/payment/add",
      {
        customerId,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    ).then((response) =>
    {
      localStorage.removeItem("jwtToken");
      window.location.reload();
      window.alert("You need to sign in again");
    })
      .catch((error) => console.log(error));
  };

  return (
    <div className="container-fluid px-1 px-md-2 px-lg-4 py-5 mx-auto">
      <DesktopHeader />
      <div className="row d-flex justify-content-center">
        <div className="col-xl-7 col-lg-8 col-md-9 col-sm-11">
          <div className="card border-0">
            <div className="row justify-content-center">
              <h3 className="mb-4">Credit Card Checkout</h3>
            </div>
            <div className="row">
              <div className="col-sm-7 border-line pb-3">
                <div className="form-group">
                  <p className="text-muted text-sm mb-0">Stripe ID</p>
                  <input type="text" name="stripeId" value={customerId} onChange={(e) => setCustomerId(e.target.value)} placeholder="Stripe ID" size="15" />
                </div>
              </div>
              <div className="col-sm-5 text-sm-center justify-content-center pt-4 pb-4">

                <small className="text-sm text-muted">Payment amount</small>
                <div className="row px-3 justify-content-sm-center">
                  <h2>
                    <span className="text-md font-weight-bold mr-2">$</span>
                    <span className="text-danger">59.99</span>
                  </h2>
                </div>
                <button type="submit" onClick={handleSubmit} className="btn btn-red text-center mt-4">PAY</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default PaymentPage;