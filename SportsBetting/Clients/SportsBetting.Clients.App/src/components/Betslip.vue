<template>
  <div>
    <b-toast id="BetslipToast">
      <template v-slot:toast-title>
        <div class="d-flex flex-grow-1 align-items-baseline">
          <b-img blank blank-color="#ff5555" class="mr-2" width="12" height="12"></b-img>
          <strong class="mr-auto">{{marketName}}</strong>
        </div>
      </template>
      <div id="BettingName">{{getOddName()}}</div>
      <div>
        <span id='BettingValue'>{{odd.Value}}</span>
        <input id="Stake" type="number" min="0" placeholder="0.00" />
        <button id="PlaceBetBtn" class="place-bet-btn" @click="placeBet()">Place Bet</button>
      </div>
    </b-toast>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  props: {
    odd: Object,
    marketName: String
  },
  data() {
    return {
        match: {}
    }
  },
  methods: {
    getOddName() {
      let oddName = this.odd.Name;

      if (this.odd.Symbol !== null && this.odd.Header > 0) {
        oddName += ' ' + this.odd.Symbol + '' + this.odd.Header;
      }
      else if (this.odd.Header > 0) {
        oddName += ' ' + this.odd.Header;
      }
      
      return oddName;
    },
    placeBet() {
      axios.post('http://localhost:64399/api/Bets', {
        username: this.registerModel.username.value,
        password: this.registerModel.password.value,
        confirmPassword: this.registerModel.confirmPassword.value,
        email: this.registerModel.email.value
      })
      .then(res => console.log(res.data))
      .catch(err => this.showModelStateErrors(err.response.data));
    }
  }
}
</script>

<style>
  #BettingValue {
    background-color:#1e5dd1;
    color: #ffffff;
    font-weight: bold;
    font-size: 18px;
    padding: 10px;
  }

  #BettingName {
    margin-bottom: 10px;
    color: #000;
    font-weight: bold;
    white-space: nowrap; 
    width: 255px; 
    overflow: hidden;
    text-overflow: ellipsis; 
  }

  #Stake {
    width: 50px;
    margin-left: 10px;
    padding: 10px;
  }

  input[type=number]::-webkit-inner-spin-button, 
  input[type=number]::-webkit-outer-spin-button { 
    -webkit-appearance: none; 
    margin: 0; 
  }

  #PlaceBetBtn {
    margin-left: 10px;
    padding: 10px;
    background-color: #cb2c2e;
    color: #ffffff;
    border: none;
    font-weight: bold;
    width: 130px;
  }

  #PlaceBetBtn:hover {
    background-color: #5baf4e;
  }
</style>