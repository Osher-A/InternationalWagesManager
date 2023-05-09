<template>
  <base-spinner v-if="!!employee && isLoading"></base-spinner>
  <base-dialog title="An error occured" :show="!!error" @close="confirmError">
    <p>{{ error }}</p>
  </base-dialog>
    <base-div>
    <form >
  <base-header title="Employee Details"></base-header>
  <base-table>
    <tbody>
      <tr>
        <th scope="row"><h6>First name:</h6></th>
        <td>
          <label class="form-control">{{ employee.firstName }}</label>
        </td>
      </tr>
      <tr>
        <th scope="row"><h6>Last name:</h6></th>
        <td>
          <label class="form-control">{{employee.lastName}}</label>
        </td>
      </tr>
      <tr>
        <th scope="row"><h6>DOB</h6></th>
        <td>
          <label class="form-control">{{date}}</label>
        </td>
      </tr>
      <tr>
        <th scope="row"><h6>Phone:</h6></th>
        <td><label class="form-control">{{employee.phone}}</label></td>
      </tr>
      <tr>
        <th scope="row"><h6>Email:</h6></th>
        <td>
          <label class="form-control">{{employee.email}}</label>
        </td>
      </tr>

    </tbody>
    <tfoot>
        <routerLink to="/employees" class="btn mt-3 backButton"  >Back</routerLink>
    </tfoot>

  </base-table>
</form> 

    </base-div>
</template>

<script>
import {getData} from '../../hooks/apiHandler'
export default{
    props:{
       id:{
        type: String,
        required: true
      }
    }, 
    data(){
        return{
            employee: null,
            isLoading: false,
            error: null
        };
    },
    
    computed: {
      date(){
        return new Date(this.employee.dob).toLocaleDateString(); // its passed as a string so needs to be converted first
      }
    },

    methods: {
      confirmError(){
        this.error = null;
      }
    },

      created(){
      //this.employee = this.$store.getters['employees/getEmployee'](1);
      this.isLoading = true;
      getData(`https://localhost:7194/api/Employees/${this.id}`).then(response => {
        this.employee = response;
        this.isLoading = false
      }).catch(error => {
        this.error = error.message || 'something went wrong !';
        this.isLoading = false;
      })
    }
}
</script>
<style scoped>
.backButton {
  background-color: #30311d;
  color: white;
  height: 40px;
  width: 100%;
}

</style>