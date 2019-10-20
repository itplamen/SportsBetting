<template>
  <div>
    <Betslip :marketName=marketName :odd=odd />
    <b-container class="container-row">
      <b-row>
        <b-col cols="5" class="team-name">{{match.HomeTeam}}</b-col>
        <b-col cols="2" class="score">{{match.Score}}</b-col>
        <b-col cols="5" class="team-name">{{match.AwayTeam}}</b-col>
      </b-row>
      <b-row>
        <b-col cols="12" class="category-name">{{match.Category}}</br>{{match.Tournament}}</b-col>
      </b-row>
    </b-container>
    <b-jumbotron >
      <b-container v-for="market in match.Markets">
          <Market :market=market v-on:showBetslip="showBetslipInfo" />
      </b-container>
    </b-jumbotron>
  </div>
</template>

<script>
import axios from 'axios'
import $ from 'jquery'
import Market from './Market'
import Betslip from './Betslip'

export default {
  components: {
    Market,
    Betslip
  },
  data() {
    return {
      match: {},
      odd: {},
      marketName: ''
    }
  },
  created() {
    $(window).off('scroll');

    axios.get(`http://localhost:64399/api/Matches/${this.$route.params.id}`)
      .then(res => this.match = res.data)
      .catch(err => console.log(err));
  },
  methods: {
    showBetslipInfo(odd, marketName) {
      this.odd = odd;
      this.marketName = marketName;
      this.$bvToast.show('BetslipToast');
    }
  }
}
</script>

<style scoped>
  .score{
    font-weight: bold;
    font-size: 24px;
    color: #ff365f;
  }

  .team-name{
    font-weight: bold;
    font-size: 18px;
  }

  .category-name {
    margin-top: 20px;
    font-weight: bold;
    font-size: 14px;
  }
</style>