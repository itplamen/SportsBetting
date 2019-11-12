<template>
  <div>
    <div>
      <span id="Username">{{getAccount.username}}</span>
      <span id="Balance">${{getAccount.balance}}</span>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  computed: mapGetters(['getAccount']),
}
</script>

<style scoped>
  #Username, #Balance {
    color: #52b290;
    font-weight: bold;
    margin-left: 10px;
    font-size: 20px;
  }
</style>