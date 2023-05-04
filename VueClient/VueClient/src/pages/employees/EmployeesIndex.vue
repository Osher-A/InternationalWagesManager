<template>
<div v-if="employees.length < 1" class="d-flex justify-content-center">
  <div class="spinner-border text-primary"  role="status" >
  </div>
</div>
<base-div v-if="employees.length > 0">
  <base-header title="Employees Index"></base-header>
  <base-table>
    <thead>
      <tr>
        <th scope="col">Employee name</th>
        <th scope="col" ></th>
      </tr>
    </thead>
    <tbody >
      <tr v-for="employee in employees" v-bind:key="employee.id">
        <td>{{employee.firstName }}</td>
        <td>
          <routerLink :to="`/employee/${employee.id}`" class="btn btn-outline-success" style="width: 45%; margin:0 3.3% 0 3.3%;">View details</routerLink>
          <a class="btn btn-outline-danger" style="width: 45%;" v-on:click="deleteEmployee(employee.id)">Delete employee</a>
        </td>
      </tr>
    </tbody>
  </base-table>
  <div style="max-width:700px">
  <routerLink to="/addEmployee" class="btn btn-primary form-control mt-3" style="width: 25%; background-color: black;" >Add Employee</routerLink>
  </div>
</base-div>
</template>
<script setup>
import { onBeforeMount, ref} from 'vue';
import { useStore} from 'vuex';
import { getData } from '../../hooks/apiHandler';
 
const store = useStore();

const employees = ref([]);
//const employeesRef = toRefs(employees);


function deleteEmployee(id) {
store.dispatch('employees/deleteEmployee', id);
employees.value = store.getters['employees/getEmployees'];
}

onBeforeMount(() =>{
//employees.value = store.getters['employees/getEmployees'];
getData('https://localhost:7194/api/Employees').then(data => {
  employees.value = data;
});
});

</script>

<style scoped>
th {
    width: 50%;
}
</style>