import {createRouter, createWebHistory} from 'vue-router';

import EmployeesIndex from './EmployeesIndex.vue'
import EmployeeDetails from './EmployeeDetails.vue'

// for lazy loading to optimize performance
const AddEmployee = () => import('./AddEmployee.vue');

const router = createRouter({
 history: createWebHistory(),
 routes: [
    {path: '/', redirect: '/employees'},
    {path: '/addEmployee', component: AddEmployee},
    {path: '/employees', component: EmployeesIndex},
    {path: '/employee/:id', component: EmployeeDetails, props: true }
 ]
});

export default router;



