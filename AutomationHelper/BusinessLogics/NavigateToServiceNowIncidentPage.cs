using AutomationHelper.Models;
using AutomationHelper.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;
using System;
using System.Collections.Generic;

namespace AutomationHelper.BusinessLogics
{
    public class NavigateToServiceNowIncidentPage
    {
        private readonly TargetUrlGetter _urlGetter;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _outputPath;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;
        private readonly ResultExporter _resultExporter;

        private IWebDriver _driver = new ChromeDriver();

        private List<IncidentResultTable> _listIncidentResultTable = new List<IncidentResultTable>();

        public NavigateToServiceNowIncidentPage(TargetUrlGetter urlGetter
            , ResultExporter resultExporter
            , string userName
            , string password
            , string outputPath
            , DateTime startDate
            , DateTime endDate
            , DateTime startTime
            , DateTime endTime)
        {
            _urlGetter = urlGetter;
            _userName = userName;
            _password = password;
            _resultExporter = resultExporter;
            _outputPath = outputPath;
            _startDate = startDate;
            _endDate = endDate;
            _startTime = startTime;
            _endTime = endTime;
        }

        public string NavigateToIncidentPage()
        {
            try
            {
                NavigateToTargetPageViaServiceNow();
                ExtractDataFromPage();
                _resultExporter.ExportAsCsvFile(_outputPath, "IncidentTicketsOverview.xlsx", _listIncidentResultTable);

                Log.Information("Finish process.");
                return $"Finish at {DateTime.Now}";
            }
            catch (Exception e)
            {
                Log.Error($"Fail. {e.Message}");
                return $"Fail, {e.Message}. - {DateTime.Now}";
            }
        }

        private void NavigateToTargetPageViaServiceNow()
        {
            Log.Information("Start navigate to incident Page.");

            _driver.Navigate().GoToUrl(_urlGetter.Incidents_OpsAppLasAnz);
            _driver.FindElement(By.Id("i0116")).SendKeys(_userName);
            _driver.FindElement(By.Id("idSIButton9")).Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.FindElement(By.Id("passwordInput")).SendKeys(_password);
            _driver.FindElement(By.Id("submitButton")).Click();
            _driver.FindElement(By.Id("idBtn_Back")).Click();
            _driver.SwitchTo().Frame("gsft_main");
        }

        private void ExtractDataFromPage()
        {
            var isHaveNextPage = true;

            do
            {
                IWebElement problemTable = _driver.FindElement(By.ClassName("list2_body"));
                var listTrElem = new List<IWebElement>(problemTable.FindElements(By.TagName("tr")));

                foreach (var elemTr in listTrElem)
                {
                    var listTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                    FillIncidentTableContents(listTdElem);
                }

                var nextButton = _driver.FindElement(By.Name("vcr_next"));

                if (nextButton.Enabled)
                {
                    nextButton.Click();
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                }
                else
                {
                    isHaveNextPage = false;
                }
            } while (isHaveNextPage);
        }

        private void FillIncidentTableContents(List<IWebElement> listTdElem)
        {
            var newRow = new IncidentResultTable();

            // todo, could new a claas for execute different template
            var openedDateTime = listTdElem[10].Text;
            if (!IsInDefinedTimeRange(openedDateTime)) return;

            for (int i = 0; i < listTdElem.Count; i++)
            {
                if (i == 2) newRow.Number = listTdElem[i].Text;
                if (i == 3) newRow.Priority = listTdElem[i].Text;
                if (i == 4) newRow.State = listTdElem[i].Text;
                if (i == 5) newRow.Client = listTdElem[i].Text;
                if (i == 6) newRow.ShortDescription = listTdElem[i].Text;
                if (i == 7) newRow.IncidentDueDate = listTdElem[i].Text;
                if (i == 8) newRow.AssignTo = listTdElem[i].Text;
                if (i == 9) newRow.AssignmentGroup = listTdElem[i].Text;
                if (i == 10) newRow.Opened = listTdElem[i].Text;
                if (i == 11) newRow.OpenedBy = listTdElem[i].Text;
            }

            _listIncidentResultTable.Add(newRow);
        }

        private bool IsInDefinedTimeRange(string targetDateTime)
        {
            var openedDate = DateTime.ParseExact(targetDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);

            return (openedDate.Date >= _startDate || openedDate.Date <= _endDate)
                && (openedDate.Hour >= _startTime.Hour || openedDate.Hour <= _endTime.Hour)
                && (openedDate.Minute >= _startTime.Minute || openedDate.Minute <= _endTime.Minute);
        }

    }
}
