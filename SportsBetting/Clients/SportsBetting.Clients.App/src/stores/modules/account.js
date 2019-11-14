import axios from 'axios'
import endpoints from '../../common/constants/endpoints'

const saveAccountToStorage = (storage, account) => {
  storage.id = account.Id;
  storage.username = account.Username;
  storage.balance = account.Balance;
  storage.loginToken = account.LoginToken;
  storage.expiration = account.Expiration;
};

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
  getAccount: (state) => {
    if (!state.account.id) {
      state.account.id = localStorage.id;
      state.account.username = localStorage.username;
      state.account.balance = parseInt(localStorage.balance);
      state.account.loginToken = localStorage.loginToken;
      state.account.expiration = localStorage.expiration;
    }

    return state.account;
  },
  isLoggedIn: (state) => {
    let expirationDate;

    if (state.account.expiration) {
      expirationDate = new Date(state.account.expiration);
    }
    else {
      expirationDate = new Date(localStorage.expiration);
    }

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
  },
  async logoutAccount({ commit }) {
    const response = await axios.post(endpoints.LOGOUT_ACCOUNT, { LoginToken: state.account.loginToken });
    
    commit('setAccount', response.data); 
  },
  async placeBet({ commit }, request) {
    const response = await axios.post(endpoints.PLACE_BET, request);
    
    let account = getters.getAccount;
    account.balance -= request.stake;

    commit('setAccount', account); 
  }
};

const mutations = {
  setAccount: (state, account) => {
    saveAccountToStorage(state.account, account);
    saveAccountToStorage(localStorage, account);
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
