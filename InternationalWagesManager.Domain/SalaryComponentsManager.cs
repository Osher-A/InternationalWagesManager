﻿using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Domain
{
    public class SalaryComponentsManager
    {
        private readonly IMapper _mapper;
        private readonly ISalaryComponentsRepository _salaryComponentsRepository;
        private SalaryManager _salaryManager;

        public SalaryComponentsManager(IMapper mapper, ISalaryComponentsRepository sComponentsRepository, 
            ISalaryRepository salaryRepository, IWConditionsRepository wConditionsRepository)
        {
            _mapper = mapper;
            _salaryComponentsRepository = sComponentsRepository;
            _salaryManager = new(_mapper, salaryRepository, wConditionsRepository);
        }

        public void AddSalaryComponents(DTO.SalaryComponents salaryComponents)
        {
            var modelSalaryComponents = _mapper.Map<DTO.SalaryComponents, Models.SalaryComponents>(salaryComponents);
            if (salaryComponents.EmployeeId != 0 && salaryComponents.TotalHours != 0)
            {
                _salaryComponentsRepository.AddSalaryComponents(modelSalaryComponents);
                _salaryManager.AddSalary(salaryComponents);
            }
        }

        public DTO.SalaryComponents LatestSalaryComponents(int employeeId)
        {
            return GetAllEmployeesSC(employeeId).OrderByDescending(sc => sc.Date).First();
        }

        public DTO.SalaryComponents SalaryComponentsToDate(int employeeId, DateTime date)
        {
            var searchByDate = GetAllEmployeesSC(employeeId).FirstOrDefault(sc => sc.Date?.Date == date.Date);
            if(searchByDate == null)
                searchByDate = GetAllEmployeesSC(employeeId).FirstOrDefault(sc => sc.Date?.Year == date.Year && sc.Date?.Month == date.Month );
            if (searchByDate == null)
                searchByDate = GetAllEmployeesSC(employeeId).OrderByDescending(sc => sc.Date).FirstOrDefault(sc => sc.Date?.Year == date.Year);
            return searchByDate ?? new DTO.SalaryComponents();
        }

        private List<SalaryComponents> GetAllEmployeesSC(int employeeId)
        {
           return _mapper.Map<List<Models.SalaryComponents>, List<DTO.SalaryComponents>>
                (_salaryComponentsRepository.GetEmployeeSalaryComponents(employeeId));
        }
    }
}