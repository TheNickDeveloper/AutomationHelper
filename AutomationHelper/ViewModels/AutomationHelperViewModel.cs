using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutomationHelper.ViewModels
{
    public class AutomationHelperViewModel : Screen
    {
        private string _userName = "Nick";
        private DateTime _startDateTime = DateTime.Now;
        private DateTime _endDateTime = DateTime.Now;
        private string _excuteStatus;

        private string _password;

        public AutomationHelperViewModel()
        {
            ExcuteStatus = "Ready";
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

        public string Password
        {
            get => _password;
        }

        public void OnPasswordChanged(PasswordBox source)
        {
            _password = source.Password;
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

        public async void ExecuteButtonClick()
        {
            ExcuteStatus = "Running";

            await RunBusinessLogics();

            ExcuteStatus = "Ready";

        }

        //todo, seperate into different class
        public async Task RunBusinessLogics()
        {
            var taskList = new List<Task>();

            var currTask = Task.Factory.StartNew(() => GetPassword());
            taskList.Add(currTask);
            await Task.Run(() => GetPassword());
            await Task.Delay(5000);
        }

        //todo, seperate into different class
        private void GetPassword()
        {
            var password = Password;
        }

        //todo, need to add selenium pacakge
        //todo, need to add log
        //todo, need to add browse function
    }
}