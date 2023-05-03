import {createStore} from 'vuex';

import employessModule from './modules/employees.js';

const store = createStore({
    modules: {
        employees: employessModule
    },

    state(){
      return {
        userId: '1'
      };
    }, 

});

export default store;

