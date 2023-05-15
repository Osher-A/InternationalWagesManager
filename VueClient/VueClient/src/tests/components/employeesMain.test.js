import {describe, test, expect} from 'vitest'
import { mount } from '@vue/test-utils'
import { ref } from 'vue'
import EmployeesMain from '../../components/EmployeesMain.vue'

describe('employessMain', () =>{
    test('when 2 employess, base-div is rendered with 2 rows', async () =>{
        const wrapper = mount(EmployeesMain, {
           setup(){
               const employees = ref([{id: '1', firstName: 'osher', lastName: 'moscovitch', dob: new Date(2000, 6, 4), phone: '02045678', email: 'oa@.com' },
               {id: '2', firstName: 'chava', lastName: 'moscovitch', dob: new Date(2005, 11, 13), phone: '14567896', email: 'c@.com' }])
               return {employees}
           }
        });
    expect(wrapper.find('[data-test="baseDiv"]').exists()).toBeTruthy();
    expect(wrapper.findAll('[data-test="employees"]')).toHaveLength(2);
    })
})