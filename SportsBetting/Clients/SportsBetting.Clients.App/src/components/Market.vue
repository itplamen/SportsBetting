<template>
  <div class="market">
    <div class="market-name">{{market.Name}}</div>
    <div v-bind:key="odd.Id" v-for="odd in market.Odds" class="odd">
      <Odd :odd=odd :marketName=market.Name v-on:showBetslip="$emit('showBetslip', odd, market.Name)" />
    </div>
  </div>
</template>

<script>
import Odd from './Odd'

export default {
  components: {
    Odd
  },
  props: {
    market: Object
  }
}
</script>

<style scoped>
  .market {
    background-color: #ff365f;
    border-radius: 10px;
    margin-bottom: 10px;
    color: #fff;
  }

  .market:after { 
    content: "";
    display: table;
    clear: both;
  }

  .market-name {
    font-weight: bold;
    font-size: 18px;
  }
</style>