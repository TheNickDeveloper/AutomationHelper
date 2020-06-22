using AutomationHelper.Models;
using AutomationHelper.ViewModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AutomationHelper.BusinessLogics
{
    public class NavigateToServiceNowPage
    {
        private readonly AutomationHelperViewModel _viewModel;
        private readonly string _targetPageUrl;
        private readonly IWebDriver _driver;
        private List<IResultTable> _listIncidentResultTable;
        private readonly IOpsAppProcessor _opsAppProcessor;

        public NavigateToServiceNowPage(AutomationHelperViewModel viewModel
            , string targetPageUrl
            , IOpsAppProcessor opsAppProcessor)
        {
            _targetPageUrl = targetPageUrl;
            _viewModel = viewModel;
            _opsAppProcessor = opsAppProcessor;

            _driver = new ChromeDriver();
            _listIncidentResultTable = new List<IResultTable>();
        }

        public List<IResultTable> GetDataFromWebPage()
        {
            NavigateToTargetPageViaServiceNow();
            _listIncidentResultTable = ExtractDataFromPage();
            return _listIncidentResultTable;
        }

        private void NavigateToTargetPageViaServiceNow()
        {
            Log.Information("Navigate to service now page.");
            _driver.Navigate().GoToUrl(_targetPageUrl);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(3);

            Log.Information("Entre user name.");
            _driver.FindElement(By.Id("i0116")).SendKeys(_viewModel.UserName);

            Log.Information("Windows authentication.");
            _driver.FindElement(By.Id("idSIButton9")).Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Log.Information("Input password.");
            _driver.FindElement(By.Id("passwordInput")).SendKeys(_viewModel.Password);

            Log.Information("Login button press.");
            _driver.FindElement(By.Id("submitButton")).Click();

            Log.Information("Not saving identification.");
            _driver.FindElement(By.Id("idBtn_Back")).Click();
        }

        private List<IResultTable> ExtractDataFromPage()
        {
            Log.Information("Grab data from web page.");

            var isHaveNextPage = true;
            var resultTable = new List<IResultTable>();

            _driver.SwitchTo().Frame("gsft_main");

            do
            {
                IWebElement problemTable = _driver.FindElement(By.ClassName("list2_body"));

                var listTrElem = new List<IWebElement>(problemTable.FindElements(By.TagName("tr")));

                foreach (var elemTr in listTrElem)
                {
                    var listTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                    var openedDateTime = _opsAppProcessor.GetOpenedDate(listTdElem);
                    if (IsInDefinedTimeRange(openedDateTime))
                    {
                        var result = _opsAppProcessor.GetRecordFromWebPage(listTdElem);
                        resultTable.Add(result);
                    }
                }

                if (_driver.FindElement(By.Name("vcr_next")).Enabled)
                {
                    Log.Information("Jump to next page.");
                    _driver.FindElement(By.Name("vcr_next")).Click();

                    Thread.Sleep(5000);
                }
                else
                {
                    isHaveNextPage = false;
                }
            } while (isHaveNextPage);

            return resultTable;
        }

        private bool IsInDefinedTimeRange(string targetDateTime)
        {
            var openedDate = DateTime.ParseExact(targetDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);

            return (openedDate.Date >= _viewModel.StartDate && openedDate.Date <= _viewModel.EndDate)
                && (openedDate.Hour >= _viewModel.StartTime.Hour && openedDate.Hour <= _viewModel.EndTime.Hour);
        }

    }
}
