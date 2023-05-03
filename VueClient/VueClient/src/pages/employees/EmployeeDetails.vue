<template>
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
        <routerLink to="/employees" class="btn btn-primary mt-3" style="width: 100%" >Back</routerLink>
    </tfoot>

  </base-table>
</form> 

    </base-div>
</template>

<script>

export default{
    props:{
       id:{
        type: String,
        required: true
      }
    }, 
    data(){
        return{
            employee:{}
        };
    },
 
    computed: {
      date(){
        return this.employee.dob.toLocaleDateString();
      }
    },

      created(){
      const id = this.id;
      console.log(this.id);
      this.employee = this.$store.getters['employees/getEmployee'](id);
      console.log(this.employee);
    }
}
</script>