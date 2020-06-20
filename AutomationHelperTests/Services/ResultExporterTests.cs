using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Xunit.Sdk;
using AutomationHelper.Models;

namespace AutomationHelper.Services.Tests
{
    [TestClass()]
    public class ResultExporterTests
    {
        private readonly ResultExporter _resultExporter;
        public ResultExporterTests()
        {
            _resultExporter = new ResultExporter();
        }

        [TestMethod()]
        public void ExportAsCsvFileTest()
        {
            var incidents = new List<IncidentResultTable>()
            {
                new IncidentResultTable
                {
                    Number = "INS1",
                    Priority = "P1",
                    State = "OK",
                    Client = "Company1",
                    ShortDescription = "Testing1",
                    IncidentDueDate = "DueDate1",
                    AssignTo = "Nick",
                    AssignmentGroup = "AZN",
                    OpenedBy = "Jack",
                    Opened = "OpenDate1"
                },
                new IncidentResultTable
                {
                    Number = "INS2",
                    Priority = "P2",
                    State = "Done",
                    Client = "Company2",
                    ShortDescription = "Testing2",
                    IncidentDueDate = "DueDate2",
                    AssignTo = "Mina",
                    AssignmentGroup = "AZN",
                    OpenedBy = "Vicky",
                    Opened = "OpenDate2"
                }
            };

            //_resultExporter.ExportAsCsvFile(incidents);
            Assert.Fail();
        }
    }
}