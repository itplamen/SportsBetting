<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">eSports</router-link> |
      <router-link to="/about">About</router-link> |
      <span v-if="!isLoggedIn">
        <router-link to="" v-b-modal.LoginModal>Login</router-link> |
        <router-link to="" v-b-modal.RegisterModal>Register</router-link>
          
          <Login />
          <Register />
      </span>
      <span v-else>
        <router-link to="">Logout</router-link>
      </span>
    </div>
    <router-view/>
  </div>
</template>

<script>
import Login from './components/Login';
import Register from './components/Register';
import { mapGetters } from 'vuex';
import { truncate } from 'fs';

export default {
  computed: mapGetters(['isLoggedIn']),
  components: {
    Login,
    Register
  },
  created() {
    this.$store.watch((state, getters) => getters.isLoggedIn, (newValue, oldValue) => {})
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