using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsBlazor.xaml
    /// </summary>
    public partial class EmployeeDetailsBlazor : Page
    {
        public EmployeeDetailsBlazor()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddWpfBlazorWebView();
            Resources.Add("services", serviceCollection.BuildServiceProvider());

            InitializeComponent();
        }
    }
}
