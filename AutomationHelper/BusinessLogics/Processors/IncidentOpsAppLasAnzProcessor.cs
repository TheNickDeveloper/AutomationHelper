using AutomationHelper.Models;
using OpenQA.Selenium;
using Serilog;
using System.Collections.Generic;

namespace AutomationHelper.BusinessLogics
{
    public class IncidentOpsAppLasAnzProcessor : IOpsAppProcessor
    {
        public string GetOpenedDate(List<IWebElement> listTdElem)
        {
            return listTdElem[10].Text;
        }

        public IResultTable GetRecordFromWebPage(List<IWebElement> listTdElem)
        {
            Log.Information($"Save record: {listTdElem[2].Text}.");

            var newRow = new IncidentResultTable();

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

            return newRow;
        }


    }
}
