﻿using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MyDbContext _db;
        public EmployeeRepository(MyDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<int> AddEmployee(Employee newEmployee)
        {
            await _db.Employees.AddAsync(newEmployee);
            _db.SaveChanges();
            return newEmployee.Id;
        }

        public void UpdateEmployee(Employee employee)
        {
            _db.Employees.Update(employee);
            _db.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }

        public async Task<Employee?> GetEmployeeAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _db.Employees.ToListAsync();
        }
    }
}