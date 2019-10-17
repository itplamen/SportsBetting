<template>
    <b-modal id="RegisterModal" title="Register" hide-footer>
         <form ref="form">
            <b-form-group :state="registerModel.username.state" label="Username" label-for="UsernameInput">
                <b-form-input :state="registerModel.username.state" v-model="registerModel.username.value" id="UsernameInput" required></b-form-input>
                <div class="invalid-feedback">{{registerModel.username.message}}</div>
            </b-form-group>

            <b-form-group :state="registerModel.email.state" label="Email" label-for="EmailInput">
                <b-form-input :state="registerModel.email.state" v-model="registerModel.email.value" id="EmailInput" required></b-form-input>
                <div class="invalid-feedback">{{registerModel.email.message}}</div>
            </b-form-group>

            <b-form-group :state="registerModel.password.state" label="Password" label-for="PasswordInput">
                <b-form-input :state="registerModel.password.state" v-model="registerModel.password.value" id="PasswordInput" type="password" required></b-form-input>
                <div class="invalid-feedback">{{registerModel.password.message}}</div>
            </b-form-group>

            <b-form-group :state="registerModel.confirmPassword.state" label="Confirm Password" label-for="ConfirmPasswordInput">
                <b-form-input :state="registerModel.confirmPassword.state" v-model="registerModel.confirmPassword.value" id="ConfirmPasswordInput" type="password" required></b-form-input>
                <div class="invalid-feedback">{{registerModel.confirmPassword.message}}</div>
            </b-form-group>
            
            <b-button block @click="register()" variant="primary">Register</b-button>
            <b-button block @click="$bvModal.hide('RegisterModal')">Close</b-button>
        </form>
    </b-modal>
</template>

<script>
import axios from 'axios'
import enums from '../common/constants/enums';
import registrationValidator from '../validators/registrationValidator';
import { mapGetters, mapActions } from 'vuex';

export default {
    computed: mapGetters(['getAccount']),
    created() {
        this.fetchAccount();
    },
    data() {
        return {
            registerModel: {
                username: {
                    value: '',
                    state: '',
                    message: ''
                },
                password: {
                    value: '',
                    state: '',
                    message: ''
                },
                confirmPassword: {
                    value: '',
                    state: '',
                    message: ''
                },
                email: {
                    value: '',
                    state: '',
                    message: ''
                }
            }
        }
    },
    methods: {
        ...mapActions(['fetchAccount']),
        register() {
            if(this.isRegisterValid()) {
                axios.post('http://localhost:64399/api/Account/Register', {
                        username: this.registerModel.username.value,
                        password: this.registerModel.password.value,
                        confirmPassword: this.registerModel.confirmPassword.value,
                        email: this.registerModel.email.value
                    })
                    .then(res => console.log(res.data))
                    .catch(err => this.showModelStateErrors(err.response.data));
            }
        },
        isRegisterValid() {
            return this.areAllFieldsFilled() && 
                this.isEmailValid() && 
                this.isPasswordConfirmed() &&
                this.$refs.form.checkValidity();
        },
        areAllFieldsFilled() {
            let validations = registrationValidator.validateFields(this.registerModel);
            let self = this;
            
            validations.forEach(function (item) {
                self.registerModel[item.key].state = item.state;
                self.registerModel[item.key].message = item.message;
            });

            return validations.every(x => x.state == enums.REGISTER_STATE.VALID);
        },
        isEmailValid() {
            let validation = registrationValidator.validateEmail(this.registerModel.email.value);
            
            this.registerModel.email.state = validation.state;
            this.registerModel.email.message = validation.message;

            return validation.state == enums.REGISTER_STATE.VALID;
        },
        isPasswordConfirmed() {
            let validation = registrationValidator.validatePasswordMatching(
                this.registerModel.password.value, 
                this.registerModel.confirmPassword.value);

            this.registerModel.confirmPassword.state = validation.state;
            this.registerModel.confirmPassword.message = validation.message;

            return validation.state == enums.REGISTER_STATE.VALID;
        },
        showModelStateErrors(data) {
            let self = this;

            Object.keys(data.ModelState).forEach(function(key) {
                let message = data.ModelState[key][0];
                let splitKey = key.toString().toLowerCase().split('.');
                let prop = splitKey.length == 1 ? splitKey : splitKey[1];

                self.registerModel[prop].message = message;
                self.registerModel[prop].state = enums.REGISTER_STATE.INVALID;
            });
        }
    }
}
</script>