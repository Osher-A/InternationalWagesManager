<template>
    <base-div>
    <form v-on:submit.prevent="submit" >
        <base-header title="New Employee"></base-header>
        <base-table>
            <tbody>
                <tr>
                    <th scope="row">First name:</th>
                    <td>
                        <input type="text" class="form-control" required minlength="2" v-model.trim="employee.firstName">
                    </td>
                </tr>
                <tr>
                    <th scope="row">Last name:</th>
                    <td>
                        <input type="text" class="form-control" required minlength="3" v-model.trim="employee.lastName" />
                    </td>
                </tr>
                <tr>
                    <th scope="row">DOB:</th>
                    <td>
                        <input type="date" class="form-control" placeholder="dd/mm/yyyy" required v-model="employee.dob">
                    </td>
                </tr>
                <tr>
                    <th scope="row">Phone:</th>
                    <td><input type="tel" class="form-control" required minlength="7" v-model="employee.tel" /></td>
                </tr>
                <tr>
                    <th scope="row">Email:</th>
                    <td>
                        <input type="text" class="form-control" required email v-model="employee.email">
                    </td>
                </tr>
                <tr>
                    <td>
                        <a type="button" class="btn btn-outline-success form-control" href="/employees">Cancel</a>
                    </td>
                    <td>
                        <button class="btn btn-outline-primary form-control">Submit</button>
                    </td>
                </tr>
            </tbody>
        </base-table>
    </form>
</base-div>
</template>

<script>
export default{
    data(){
        return {
            employee:{
            id: 3,
            firstName: '',
            lastName: '',
            dob: '',
            tel: '',
            email: ''
            }
        };
    },
    methods:{
     submit(){
       this.$store.dispatch('employees/addEmployee', this.employee);
     },
    }
}
</script>