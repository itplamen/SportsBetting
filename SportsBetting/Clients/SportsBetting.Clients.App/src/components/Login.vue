<template>
  <b-modal id="LoginModal" title="Log In" hide-footer>
    <form ref="form">
      <b-form-group :state="loginModel.username.state" label="Username" label-for="UsernameInput">
        <b-form-input :state="loginModel.username.state" v-model="loginModel.username.value" id="UsernameInput" required></b-form-input>
        <div class="invalid-feedback">{{loginModel.username.message}}</div>
      </b-form-group>

      <b-form-group :state="loginModel.password.state" label="Password" label-for="PasswordInput">
        <b-form-input :state="loginModel.password.state" v-model="loginModel.password.value" id="PasswordInput" type="password" required></b-form-input>
        <div class="invalid-feedback">{{loginModel.password.message}}</div>
      </b-form-group>

      <b-form-group>
        <b-form-checkbox id="RememberMeCheckBox" v-model="rememberMe">
          Remember Me
        </b-form-checkbox>
      </b-form-group>
      
      <b-button block @click="login()" variant="primary">Log In</b-button>
      <b-button block @click="$bvModal.hide('LoginModal')">Close</b-button>
  </form>
  </b-modal>
</template>

<script>
import enums from '../common/constants/enums';
import registrationValidator from '../validators/registrationValidator';
import { mapActions } from 'vuex';

export default {
  data() {
    return {
      loginModel: {
        username: {
          value: '',
          state: '',
          message: ''
        },
        password: {
          value: '',
          state: '',
          message: ''
        }
      },
      rememberMe: false
    }
  },
  methods: {
    ...mapActions(['loginAccount']),
    login() {
      if(this.isLoginValid()) {
        this.loginAccount({
          username: this.loginModel.username.value,
          password: this.loginModel.password.value,
          rememberMe: this.rememberMe
        })
        .then(res => this.$bvModal.hide('LoginModal'))
        .catch(err => this.showModelStateErrors(err.response.data));
      }
    },
    isLoginValid() {
      return this.areAllFieldsFilled() && this.$refs.form.checkValidity();
    },
    areAllFieldsFilled() {
      let validations = registrationValidator.validateFields(this.loginModel);
      let self = this;
      
      validations.forEach(function (item) {
        self.loginModel[item.key].state = item.state;
        self.loginModel[item.key].message = item.message;
      });

      return validations.every(x => x.state == enums.REGISTER_STATE.VALID);
    },
    showModelStateErrors(data) {
      let self = this;

      Object.keys(data.ModelState).forEach(function(key) {
        let message = data.ModelState[key][0];
        let splitKey = key.toString().toLowerCase().split('.');
        let prop = splitKey.length == 1 ? splitKey : splitKey[1];

        self.loginModel[prop].message = message;
        self.loginModel[prop].state = enums.REGISTER_STATE.INVALID;
      });
    }
  }
}
</script>