export default{
  namespaced: true,
  
  state(){
    return {
      employees: [
          {id: '1', firstName: 'osher', lastName: 'moscovitch', dob: new Date(2000, 6, 4), phone: '02045678', email: 'oa@.com' },
          {id: '2', firstName: 'chava', lastName: 'moscovitch', dob: new Date(2005, 11, 13), phone: '14567896', email: 'c@.com' }
      ]
    };
  }, 

  getters:{
    getEmployees(state){
      return state.employees;
    },
    getEmployee: (state) => (payLoad) => {
      return state.employees.find(employee =>{
        return employee.id === payLoad;
      })
    }
  },

  mutations: {
    addEmployee(state, payLoad){
      state.employees.push(payLoad)
    },

    deleteEmployee(state, payLoad){
      state.employees = payLoad;
    }

  }, 
  actions: {
    addEmployee(context, payLoad){
      context.commit('addEmployee', payLoad)
    },

    deleteEmployee(context, payLoad){
      const remainingEmployees = context.getters.getEmployees.filter((e)=>{
        return e.id != payLoad;
      })
       context.commit('deleteEmployee', remainingEmployees);
       console.log(remainingEmployees);
    } 
  }

}