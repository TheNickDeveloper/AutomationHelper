using AutomationHelper.Models;
using AutomationHelper.Services;
using Caliburn.Micro;
using System;
using System.Threading.Tasks;

namespace AutomationHelper.ViewModels
{
    public class AutomationHelperViewModel : Screen
    {
        private string _userName = "Nick";
        private DateTime _startDateTime = DateTime.Now;
        private DateTime _endDateTime = DateTime.Now;
        private string _excuteStatus;
        private string _password;
        private string _exportDataPath;
        private readonly PathBrowseHelper _pathBrowseHelper;
        private readonly LoginInfoPage _loginInfoPage;

        public AutomationHelperViewModel()
        {
            ExcuteStatus = "Ready";
            _password = string.Empty;
            _pathBrowseHelper = new PathBrowseHelper();
            _loginInfoPage = new LoginInfoPage();
        }

        public string ExcuteStatus
        {
            get => _excuteStatus;
            set
            {
                _excuteStatus = value;
                NotifyOfPropertyChange(() => ExcuteStatus);
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public DateTime StartDateTime
        {
            get => _startDateTime;
            set
            {
                _startDateTime = value;
                NotifyOfPropertyChange(() => StartDateTime);
            }
        }

        public DateTime EndDateTime
        {
            get => _endDateTime;
            set
            {
                _endDateTime = value;
                NotifyOfPropertyChange(() => EndDateTime);
            }
        }

        public string Password
        {
            get => _password;
        }

        public string ExportDataPath
        {
            get => _exportDataPath;
            set
            {
                _exportDataPath = value;
                NotifyOfPropertyChange(() => ExportDataPath);
            }
        }

        public void BrowseButtonClickExportDataPath()
        {
            ExportDataPath = _pathBrowseHelper.FolderPathBrowser(ExportDataPath);
        }

        public async void ExecuteButtonClick()
        {
            ExcuteStatus = "Running";

            await RunBusinessLogics();

            ExcuteStatus = $"Finish at {DateTime.Now}";
        }

        public async Task RunBusinessLogics()
        {
            await Task.Run(() => SearchResultViaBingDemo());
        }

        private void SearchResultViaBingDemo()
        {
            // get password triggered
            var password = Password;

            var businessLogicDemo = new BusinessLogicDemo(_loginInfoPage);
            businessLogicDemo.SearchResultViaBingDemo();
        }

        //todo, need to add selenium pacakge
        //todo, need to add log
        //todo, need to add browse function
    }
}