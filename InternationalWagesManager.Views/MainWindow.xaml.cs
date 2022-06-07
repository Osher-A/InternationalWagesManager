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
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Views.Pages;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace InternationalWagesManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IEmployeeRepository _employeeRepository;
        private IMapper _mapper;
        public MainWindow(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            InitializeComponent();
        }

        private void MainWindowFrame_Loaded(object sender , RoutedEventArgs e)
        {
            // MainWindowFrame.Content = new HomePage(_employeeRepository, _mapper);
        }

        private void TreeViewAddEmployee_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new EmployeeDetails();
        }
        private void TreeViewAddWorkConditions_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new WorkConditions();
        }

       
    }
}
