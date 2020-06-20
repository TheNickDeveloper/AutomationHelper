using AutomationHelper.BusinessLogics;
using AutomationHelper.Models;
using AutomationHelper.Services;
using Caliburn.Micro;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string _ticketOptions;
        private readonly PathBrowseHelper _pathBrowseHelper;
        private readonly TicketSourceGetter _sourceGetter;
        private readonly ResultExporter _resultExporter;


        public AutomationHelperViewModel()
        {
            ExcuteStatus = "Ready";
            _password = string.Empty;
            _pathBrowseHelper = new PathBrowseHelper();
            _sourceGetter = new TicketSourceGetter();
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

        public string Password
        {
            get => _password;
        }

        public void OnPasswordChanged(PasswordBox source)
        {
            _password = source.Password;
        }

        public List<string> TicketTypes
        {
            get => _sourceGetter.TicketTypes;
        }

        public string TicketOptions
        {
            get => _ticketOptions;
            set
            {
                _ticketOptions = value;
                NotifyOfPropertyChange(() => TicketOptions);
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

                try
                {
                    // intialize log file
                    Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.File(ConfigurationManager.AppSettings["LogFolderPath"].ToString()
                        , rollingInterval: RollingInterval.Day
                        , rollOnFileSizeLimit: true)
                    .CreateLogger();

                    var startDateTime = StartDate.AddHours(StartTime.Hour).AddMinutes(StartTime.Minute);
                    var endDateTime = EndDate.AddHours(EndTime.Hour).AddMinutes(EndTime.Minute);
                    var resultExporter = new ResultExporter();

                    NavigateToServiceNowPage businessLogicDemo;
                    switch (TicketOptions)
                    {
                        case "Incidents_OpsAppLasAnz":
                            Log.Information($"Start getting Incidents_OpsAppLasAnz data.");
                            businessLogicDemo = new NavigateToServiceNowPage(this
                                , _sourceGetter.Url_Incidents_OpsAppLasAnz
                                , new IncidentOpsAppLasAnzProcessor());

                            resultExporter.ExportAsCsvFile<IncidentResultTable>(ExportDataPath
                                , "IncidentTicketsOverview.xlsx"
                                , businessLogicDemo.GetDataFromWebPage());
                            break;

                        case "Problems_OpsAppLasAnz":
                            Log.Information($"Start getting Problems_OpsAppLasAnz data.");
                            businessLogicDemo = new NavigateToServiceNowPage(this
                                , _sourceGetter.Url_Problems_OpsAppLasAnz
                                , new ProblemOpsAppLasAnzProcessor());

                            resultExporter.ExportAsCsvFile<ProblemResultTable>(ExportDataPath
                                , "ProblemTicketsOverview.xlsx"
                                , businessLogicDemo.GetDataFromWebPage());
                            break;

                        // todo, Test
                        case "Test":
                            Log.Information($"Start getting Incidents_OpsAppLasAnz data.");
                            businessLogicDemo = new NavigateToServiceNowPage(this
                                , _sourceGetter.Test
                                , new IncidentOpsAppLasAnzProcessor());

                            resultExporter.ExportAsCsvFile<IncidentResultTable>(ExportDataPath
                                , "Test.xlsx"
                                , businessLogicDemo.GetDataFromWebPage());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Error. {e.Message}");
                    return $"Fail, {e.Message}. - {DateTime.Now}";
                }

                Log.Information("Finish process.");
                return $"Finish at {DateTime.Now}";
            });
        }
    }
}