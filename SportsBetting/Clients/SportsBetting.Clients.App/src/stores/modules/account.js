import axios from 'axios';

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
    async fetchAccount({ commit }) {
        const response = await axios.get('http://localhost:64399/api/Account');

        commit('setAccount', response);
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