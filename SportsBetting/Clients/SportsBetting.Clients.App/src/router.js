import Esports from './components/Esports'
import Match from './components/Match'
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
      component: Esports
    },
    {
      path: '/matches/:id',
      name: 'match',
      component: Match
    }
  ]
});