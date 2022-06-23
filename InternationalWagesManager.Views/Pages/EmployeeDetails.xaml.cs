﻿using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternationalWagesManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetails : Page
    {
        public EmployeeDetails(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            this.DataContext = new EmployeeDetailsVM(mapper, employeeRepository);
            InitializeComponent();
        }
    }
}
