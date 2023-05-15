<template>
  <base-spinner data-test="baseSpinner" v-if="isLoading"></base-spinner>
  <base-dialog data-test="baseDialog" title="An error occured" :show="!!error" @close="confirmError">
    <p>{{ error }}</p>
  </base-dialog>
  <base-dialog :show="showWarningDialog" title="Warning!">
    <h4 class="text-center"><br />You are about to permently delete an employee!<br /><br />Are you sure!</h4>
    <template v-slot:actions>
      <button v-on:click="deletingConfirmed" class="btn btn-primary">Ok</button>
      <button v-on:click="closeWarningDialog" class="btn btn-primary">cancel</button>
    </template>
  </base-dialog>
 <employees-main v-show="isVisible" ref="childComponent"
  @is-loading="loadingStatus"
   @error-message="errorMessage"
    @delete="deleteRequest">
    </employees-main>
</template>
<script >
import { ref, computed } from 'vue';
//import { useStore} from 'vuex';

import EmployeesMain  from '../../components/EmployeesMain.vue'
import { deleteData } from '../../hooks/apiHandler';
export default {
  components: { 
    'employees-main': EmployeesMain},

  setup() {
    //const store = useStore();
    const childComponent = ref(null);
    const error = ref(null);
    const isLoading = ref(false);
    const showWarningDialog = ref(false);
    let employeeId = 0;

    const isVisible = computed(() => {
      return childComponent.value != null && isLoading.value === false;
    })

    //const employeesRef = toRefs(employees);
     function loadingStatus(){
       isLoading.value =  !isLoading.value;
     }

    function confirmError() {
      error.value = null;
    }

    function deleteRequest(id){
      showWarningDialog.value = true;
      employeeId = id;
    }

    function deletingConfirmed() {
      showWarningDialog.value = false;
      deleteData('https://localhost:7194/api/Employees/' + employeeId).then(() => {
         childComponent.value.loadData();
      }).catch(error => {
        errorMessage(error.message)
      });
    }

    function closeWarningDialog() {
      showWarningDialog.value = false;
    }

   
    function errorMessage(message) {
      error.value = message || 'something went wrong';
      isLoading.value = false;
    }
    return {
      loadingStatus, error ,errorMessage, isLoading, deleteRequest, showWarningDialog, isVisible,
      confirmError, deletingConfirmed, closeWarningDialog, childComponent
    }
  }
}
</script>

<style scoped>

p {
  font-size: large;
  font-weight: bold;
}

button{
  width: 80px;
  margin-right: 5px;
  background-color: #0c0738;
}
</style>