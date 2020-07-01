using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Xunit.Sdk;
using AutomationHelper.Models;
using System.IO;

namespace AutomationHelper.Services.Tests
{
    [TestClass()]
    public class ResultExporterTests
    {
        private readonly ResultExporter _resultExporter;
        private readonly string _path;
        private readonly string _fileName;

        public ResultExporterTests()
        {
            _resultExporter = new ResultExporter();
            _path = @"C:\Users\tsainic\OneDrive - Mars Inc\Desktop\test";
            _fileName = "Test";
        }

        [TestMethod()]
        public void ExportAsCsvFileTest()
        {
            var incidents = new List<IResultTable>()
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

            _resultExporter.ExportAsCsvFile<IncidentResultTable>(_path, _fileName, incidents);
            var csvPath = _path + @$"\{_fileName}.csv";
            StreamReader r = new StreamReader(csvPath);
            var csvContents = r.ReadToEnd();
            Assert.IsTrue(csvContents.Length > 0);
        }

        [TestMethod()]
        public void ExportAsJsonFileTest()
        {
            var incidents = new List<IResultTable>()
            {
                new ProblemResultTable
                {
                    Number = "INS1",
                    Priority = "P1",
                    State = "OK",
                    ConfigurationItem = "Cconfig1",
                    ShortDescription = "Testing1",
                    AssignedTo = "Nick",
                    AssignmentGroup = "AZN",
                    OpenedBy = "Jack",
                    Opened = "OpenDate1",
                    DueDate = "DueDate1",
                    BreachFlag = "BranchFlag1",
                    Closed = "Closed01",
                    ClosedBy = "Mina1",
                    ClosureCode = "ClosureCode01",
                },
                new ProblemResultTable
                {
                    Number = "INS2",
                    Priority = "P2",
                    State = "OK",
                    ConfigurationItem = "Cconfig2",
                    ShortDescription = "Testing2",
                    AssignedTo = "Nick2",
                    AssignmentGroup = "AZN2",
                    OpenedBy = "Jack2",
                    Opened = "OpenDate2",
                    DueDate = "DueDate2",
                    BreachFlag = "BranchFlag2",
                    Closed = "Closed02",
                    ClosedBy = "Mina2",
                    ClosureCode = "ClosureCode02",
                }
            };

            _resultExporter.ExportAsJsonFile<ProblemResultTable>(_path, _fileName, incidents);

            var jsonFilePath = _path + @$"\{_fileName}.json";
            StreamReader r = new StreamReader(jsonFilePath);
            var jsonContents = r.ReadToEnd();
            Assert.IsTrue(jsonContents.Length > 0);
        }
    }
}