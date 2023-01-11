using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.Models;
using InternationalWagesManager.ViewModels;
using InternationalWagesManager.ViewModels.WorkConditons;
using System.Collections.Generic;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages.WorkConditions
{
    /// <summary>
    /// Interaction logic for WCDetails.xaml
    /// </summary>
    public partial class WCDetails : Page
    {
        public WCDetails(int id, ActionType actionType, WorkConditionsManager workConditionsManager, CurrenciesManager currenciesManager)
        {
            this.DataContext = new WCDetailsVM(id, actionType, workConditionsManager, currenciesManager);
            InitializeComponent();
        }
    }
}
