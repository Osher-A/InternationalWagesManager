<template>
  <base-spinner v-if="isVisible"></base-spinner>
  <base-dialog title="An error occured" :show="!!error" @close="confirmError">
    <p>{{ error }}</p>
  </base-dialog >
  <base-dialog :show="showWarningDialog" title="Warning!">
  <h4 class="text-center"><br/>You are about to permently delete an employee!<br/><br/>Are you sure!</h4>
  <template v-slot:actions>
    <button v-on:click="deletingConfirmed">Ok</button>
    <button v-on:click="closeWarningDialog">cancel</button>
  </template>  
</base-dialog>
  <base-div v-if="employees.length > 0">
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
          <td>{{ employee.firstName }}</td>
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
      <routerLink to="/addEmployee" class="btn form-control mt-3 addButton" >Add Employee</routerLink>
    </div>
</base-div></template>
<script setup>
import { onBeforeMount, ref, computed} from 'vue';
//import { useStore} from 'vuex';
import { getData , deleteData } from '../../hooks/apiHandler';
 
//const store = useStore();

const employees = ref([]);
const error = ref(null);
const isLoading = ref(false);
const showWarningDialog = ref(false);
let employeeId = 0;

const isVisible = computed(()=>{
  return employees.value.length < 1 && isLoading.value;
})
//const employeesRef = toRefs(employees);

function confirmError(){
  error.value = null;
}

function deletingConfirmed(){
showWarningDialog.value = false;
isLoading.value = true;
deleteData('https://localhost:7194/api/Employees/' + employeeId).then(()=> {
 loadData();
}).catch(error => {
  errorMessage(error.message)
});
}

function closeWarningDialog(){
  showWarningDialog.value = false;
}

function deleteEmployee(id) {
//store.dispatch('employees/deleteEmployee', id);
//employees.value = store.getters['employees/getEmployees'];
showWarningDialog.value = true;
employeeId = id;
}

onBeforeMount(() =>{
//employees.value = store.getters['employees/getEmployees'];
loadData();
});

function loadData(){
  isLoading.value = true;
  getData('https://localhost:7194/api/Employees').then(data => {
  employees.value = data;
}).catch(e => {
  errorMessage(e.message)
});
}

function errorMessage(message){
  error.value = message || 'something went wrong';
isLoading.value = false;
}

</script>

<style scoped>
th {
    width: 50%;
}
p {
  font-size:large;
  font-weight: bold;
}
.tableButton{
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