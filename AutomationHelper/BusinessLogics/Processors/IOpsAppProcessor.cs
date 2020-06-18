using AutomationHelper.Models;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationHelper.BusinessLogics
{
    public interface IOpsAppProcessor
    {
        IResultTable GetRecordFromWebPage(List<IWebElement> listTdElem);
        string GetOpenedDate(List<IWebElement> listTdElem);
    }
}