import Match from './components/Match'
import Matches from './components/Matches'
import Router from 'vue-router'
import Vue from 'vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Matches
    },
    {
      path: '/matches/:id',
      name: 'match',
      component: Match
    }
  ]
});