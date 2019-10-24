import axios from 'axios'
import endpoints from '../../common/constants/endpoints'

const state = {
  account: {
    id: '',
    username: '',
    balance: 0,
    loginToken: '',
    expiration: ''
  }
};

const getters = {
  getAccount: (state) => state.account,
  isLoggedIn: (state) => {
    let expirationDate = new Date(state.account.expiration);
    let dateNow = new Date();
    
    return expirationDate > dateNow;
  }
};

const actions = {
  async registerAccount({ commit }, request) {
    const response = await axios.post(endpoints.REGISTER_ACCOUNT, request);
    
    commit('setAccount', response.data);    
  },
  async loginAccount({ commit }, request) {
    const response = await axios.post(endpoints.LOGIN_ACCOUNT, request);
    
    commit('setAccount', response.data); 
  }
};

const mutations = {
  setAccount: (state, account) => {
    state.account.id = account.Id;
    state.account.username = account.Username;
    state.account.balance = account.Balance;
    state.account.loginToken = account.LoginToken;
    state.account.expiration = account.Expiration;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
