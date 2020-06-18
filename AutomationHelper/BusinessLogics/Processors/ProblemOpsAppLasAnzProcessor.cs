using AutomationHelper.Models;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomationHelper.BusinessLogics
{
    class ProblemOpsAppLasAnzProcessor : IOpsAppProcessor
    {
        public string GetOpenedDate(List<IWebElement> listTdElem)
        {
            return listTdElem[8].Text;
        }

        public IResultTable GetRecordFromWebPage(List<IWebElement> listTdElem)
        {
            Log.Information($"Save record: {listTdElem[2].Text}.");

            var newRow = new ProblemResultTable();

            for (int i = 0; i < listTdElem.Count; i++)
            {
                if (i == 2) newRow.Number = listTdElem[i].Text;
                if (i == 3) newRow.Priority = listTdElem[i].Text;
                if (i == 4) newRow.ConfigurationItem = listTdElem[i].Text;
                if (i == 5) newRow.ShortDecription = listTdElem[i].Text;
                if (i == 6) newRow.AssignedTo = listTdElem[i].Text;
                if (i == 7) newRow.AssignmentGroup = listTdElem[i].Text;
                if (i == 8) newRow.Opened = listTdElem[i].Text;
                if (i == 9) newRow.OpenedBy = listTdElem[i].Text;
                if (i == 10) newRow.DueDate = listTdElem[i].Text;
                if (i == 11) newRow.BreachFlag = listTdElem[i].Text;
                if (i == 12) newRow.Closed = listTdElem[i].Text;
                if (i == 13) newRow.ClosedBy = listTdElem[i].Text;
                if (i == 14) newRow.State = listTdElem[i].Text;
                if (i == 15) newRow.ClosureCode = listTdElem[i].Text;
            }

            return newRow;
        }

    }
}
