<template>
<base-div data-test="baseDiv" v-if="employees?.length > 0">
    <base-header title="Employees Index"></base-header>
    <base-table>
      <thead>
        <tr>
          <th scope="col">Employee name</th>
          <th scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="employee in employees" v-bind:key="employee.id">
          <td data-test="employees">{{ employee.firstName }}</td>
          <td>
            <routerLink :to="`/employee/${employee.id}`" class="btn btn-outline-success tableButton"
              style="margin:0 3.3% 0 3.3%;">View details</routerLink>
            <a class="btn btn-outline-danger tableButton" v-on:click="deleteEmployee(employee.id)">Delete
              employee</a>
          </td>
        </tr>
      </tbody>
    </base-table>
    <div style="max-width:700px">
      <routerLink to="/addEmployee" class="btn form-control mt-3 addButton">Add Employee</routerLink>
    </div>
</base-div>
</template>

<script setup>

import { ref, onBeforeMount } from 'vue'
import { getData} from '../../src/hooks/apiHandler'

    const emit = defineEmits(['is-loading', 'error-message', 'delete']);
    
    const employees = ref([]);
     
    defineExpose({employees, loadData})

    onBeforeMount(() => {
      //employees.value = store.getters['employees/getEmployees'];
      loadData();
    });
  
    function loadData() {
      loadingStatus()
      getData('https://localhost:7194/api/Employees').then(data => {
        employees.value = data;
        console.log('child from child')
        console.log(employees)
        loadingStatus();
      }).catch(e => {
        emit('error-message', e.message)
      });
    }

    function loadingStatus(){
        emit('is-loading');
    }
    
    function deleteEmployee(id) {
      //store.dispatch('employees/deleteEmployee', id);
      //employees.value = store.getters['employees/getEmployees'];
      emit('delete', id)
    }
</script>

<style scoped>
th {
  width: 50%;
}

.tableButton {
  width: 45%;
}

.addButton {
  margin-left: 5px;
  background-color: #30311d;
  color: white;
  height: 40px;
  width: 25%;
}
</style>