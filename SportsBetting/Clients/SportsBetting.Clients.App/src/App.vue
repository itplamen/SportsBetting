<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">eSports</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="" v-b-modal.RegisterModal>Register</router-link>
    </div>
    <router-view/>
    <Register />
  </div>
</template>

<script>
import Register from './components/Register'
import { truncate } from 'fs';

export default {
  components: {
    Register
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>