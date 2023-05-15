import { mount } from '@vue/test-utils'
import {describe, test, expect} from 'vitest'
import  EmployeeIndex from '../../pages/employees/EmployeesIndex.vue'
import { ref } from 'vue'

describe('employeesIndex', () => {
    test('when error, base-dialog is rendered', () => {
        const wrapper = mount (EmployeeIndex, {
            setup(){
                const error = ref(true);
                return { error }
            }
        });
        expect(wrapper.find('[data-test="baseDialog"]').exists()).toBeTruthy();
       })

       test('when loading, base-spinner is rendered', () => {
        const wrapper = mount (EmployeeIndex, {
            setup(){
                const isLoading = ref(true);
                return { isLoading }
            }
        });
        expect(wrapper.find('[data-test="baseSpinner"]').exists()).toBeTruthy();
       })
     
    test('when less then one employee, base-div not rendered', async () =>{
        const wrapper = mount(EmployeeIndex, {
           setup(){
               const employees = ref([]);
               return {employees}
        }});
   
        expect(wrapper.find('[data-test="baseDiv"]').exists()).toBeFalsy();
       })

       
})
