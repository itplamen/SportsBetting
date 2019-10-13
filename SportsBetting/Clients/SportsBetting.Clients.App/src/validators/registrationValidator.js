import enums from '../common/constants/enums';
import messageConstants from '../common/constants/messageConstants';

const validateFields = (registerModel) => {
    let validations = [];
    
    for (var key in registerModel) {
        if (registerModel.hasOwnProperty(key)) {
            let validation = {
                key: key,
                state: enums.REGISTER_STATE.VALID
            };
            
            if(!registerModel[key].value) {
                validation.state = enums.REGISTER_STATE.INVALID;
                validation.message = messageConstants.REGISTER_MESSAGE.REQUIRED_FIELD;
            }

            validations.push(validation);
        }
    }
    
    return validations;
};

const validateEmail = (email) => {
    const regex = /\S+@\S+\.\S+/;
    let validation = {
        message: '',
        state: enums.REGISTER_STATE.VALID        
    };

    if(!regex.test(email)) {
        validation.state = enums.REGISTER_STATE.INVALID;
        validation.message = messageConstants.REGISTER_MESSAGE.INVALID_EMAIL;
    }

    return validation;
};

const validatePasswordMatching = (password, confirmPassword) => {
    let validation = {
        message: '',
        state: enums.REGISTER_STATE.VALID        
    };

    if(password !== confirmPassword) {
        validation.state = enums.REGISTER_STATE.INVALID;
        validation.message = messageConstants.REGISTER_MESSAGE.CONFIRM_PASSWORD;
    }

    return validation;
};

export default {
    validateEmail,
    validateFields,
    validatePasswordMatching
};