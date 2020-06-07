using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace AutomationHelper.Models
{
    public class LoginInfoPage
    {
        [FindsBy(How = How.Id, Using = "est_cn")]
        public IWebElement DomesticSearch { get; set; }

        [FindsBy(How = How.Id, Using = "sb_form_q")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.Id, Using = "sb_form_go")]
        public IWebElement SearchButton { get; set; }

    }
}
