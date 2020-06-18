namespace AutomationHelper.Models
{
    public class ProblemResultTable :IResultTable
    {
        public string Number { get; set; }
        public string Priority { get; set; }
        public string ConfigurationItem { get; set; }
        public string ShortDecription { get; set; }
        public string AssignedTo { get; set; }
        public string AssignmentGroup { get; set; }
        public string Opened { get; set; }
        public string OpenedBy { get; set; }
        public string DueDate { get; set; }
        public string BreachFlag { get; set; }
        public string Closed { get; set; }
        public string ClosedBy { get; set; }
        public string State { get; set; }
        public string ClosureCode { get; set; }
    }
}
