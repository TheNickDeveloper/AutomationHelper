using AutomationHelper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;

namespace AutomationHelper.Services
{
    public class BusinessLogicDemo
    {
        private readonly LoginInfoPage _loginInfoPage;

        public BusinessLogicDemo(LoginInfoPage loginInfoPage)
        {
            _loginInfoPage = loginInfoPage;
        }

        public void SearchResultViaBingDemo()
        {
            try
            {
                using (IWebDriver driver = new ChromeDriver())
                {
                    driver.Navigate().GoToUrl("https://www.bing.com/");

                    PageFactory.InitElements(driver, _loginInfoPage);

                    _loginInfoPage.DomesticSearch.Click();
                    _loginInfoPage.SearchInput.SendKeys("Southampton");
                    _loginInfoPage.SearchButton.Click();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
