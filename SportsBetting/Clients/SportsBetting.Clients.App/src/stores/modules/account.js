import axios from 'axios'
import endpoints from '../../common/constants/endpoints'

const state = {
  account: {
    id: '',
    username: '',
    balance: 0
  }
};

const getters = {
  getAccount: (state) => state.account
};

const actions = {
  async registerAccount ({ commit }, request) {
    const response = await axios.post(endpoints.REGISTER_ACCOUNT, request)

    commit('setAccount', response.data)
  },
  async fetchAccount ({ commit }) {
    const response = await axios.get('http://localhost:64399/api/Account')

    commit('setAccount', response)
  }
};

const mutations = {
  setAccount: (state, account) => (state.account = account)
};

export default {
  state,
  getters,
  actions,
  mutations
};
