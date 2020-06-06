using AutomationHelper.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
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
