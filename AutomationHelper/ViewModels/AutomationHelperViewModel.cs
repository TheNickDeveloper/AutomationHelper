using AutomationHelper.BusinessLogics;
using AutomationHelper.Models;
using AutomationHelper.Services;
using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutomationHelper.ViewModels
{
    public class AutomationHelperViewModel : Screen
    {
        private string _userName;
        private DateTime _startDate = DateTime.Now.Date;
        private DateTime _endDate = DateTime.Now.Date;
        private string _excuteStatus;
        private string _password;
        private string _exportDataPath;
        private DateTime _startTime;
        private DateTime _endTime;
        private readonly PathBrowseHelper _pathBrowseHelper;
        private readonly TargetUrlGetter _urlGetter;
        private readonly ResultExporter _resultExporter;


        public AutomationHelperViewModel()
        {
            ExcuteStatus = "Ready";
            _password = string.Empty;
            _pathBrowseHelper = new PathBrowseHelper();
            _urlGetter = new TargetUrlGetter();
            _resultExporter = new ResultExporter();
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

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                NotifyOfPropertyChange(() => StartDate);
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                NotifyOfPropertyChange(() => EndDate);
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                NotifyOfPropertyChange(() => StartTime);
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                NotifyOfPropertyChange(() => EndTime);
            }
        }

        public string Password
        {
            get => _password;
        }

        public void OnPasswordChanged(PasswordBox source)
        {
            _password = source.Password;
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

        // button binding
        public async void ExecuteButtonClick()
        {
            ExcuteStatus = "Running";
            ExcuteStatus = await RunBusinessLogics();
        }

        public async Task<string> RunBusinessLogics()
        {
            return await Task.Run(() => {
                var startDateTime = StartDate.AddHours(StartTime.Hour).AddMinutes(StartTime.Minute);
                var endDateTime = EndDate.AddHours(EndTime.Hour).AddMinutes(EndTime.Minute);

                var businessLogicDemo = new NavigateToServiceNowIncidentPage(_urlGetter
                    , _resultExporter
                    , UserName
                    , Password
                    , ExportDataPath
                    , StartDate
                    , EndDate
                    , StartTime
                    , EndTime);

                return businessLogicDemo.NavigateToIncidentPage();
            });
        }
    }
}