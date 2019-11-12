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
  getAccount: (state) => state.account,
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
    
    console.log(response.data);

    commit('setAccount', response.data); 
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
