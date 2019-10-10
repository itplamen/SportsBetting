<template>
    <div>
        <b-toast id="PlaceBetToast">
            <template v-slot:toast-title>
                <div class="d-flex flex-grow-1 align-items-baseline">
                    <b-img blank blank-color="#ff5555" class="mr-2" width="12" height="12"></b-img>
                    <strong class="mr-auto">{{marketName}}</strong>
                </div>
            </template>
            <div id="BettingName">{{oddName}}</div>
            <div>
                <span id='BettingValue'>{{oddValue}}</span>
                <input id="Stake" type="number" min="0" placeholder="0.00" />
                <button id="PlaceBetBtn" class="place-bet-btn" @click="placeBet()">Place Bet</button>
            </div>
        </b-toast>
    </div>
</template>

<script>
export default {
    props: {
        marketName: '',
        oddName: '',
        oddValue: 0,
        oddId: ''
    },
    methods: {
        placeBet() {
            alert('ada')
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