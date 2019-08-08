import Router from 'vue-router'
import UpcomingGames from './components/UpcomingGames'
import Vue from 'vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: UpcomingGames
    }
  ]
})