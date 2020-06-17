using AutomationHelper.ViewModels;
using Caliburn.Micro;
using System.Windows;

namespace AutomationHelper
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sneder, StartupEventArgs e)
        {
            DisplayRootViewFor<AutomationHelperViewModel>();
        }
    }
}
