import React from "react";
import "../components/Land.css";
import Logo from '../assets/logo.png'

export default function LandPage() {
  return (
    <div id="container">
      <div class="text__wrapper">
        <div class="heading">
          <img src={Logo} alt="NetWorked" />
        </div>
        <p class="description">
          NetWorked - The best social networking site designed specifically for
          the business community
        </p>
      </div>
    </div>
  );
}
