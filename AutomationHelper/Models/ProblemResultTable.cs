namespace AutomationHelper.Models
{
    public class ProblemResultTable : IResultTable
    {
        private string _opendBy = string.Empty;
        private string _assignedTo = string.Empty;
        private string _closedBy = string.Empty;
        private string _shortDescription = string.Empty;

        public string Number { get; set; }
        public string Priority { get; set; }
        public string ConfigurationItem { get; set; }
        public string ShortDecription
        {
            get => _shortDescription;
            set
            {
                value = value.Replace(',', '.');
            }
        }

        public string AssignedTo
        {
            get => _assignedTo;
            set
            {
                var fullName = value.Split(',');
                _assignedTo = fullName.Length == 2
                    ? $"{fullName[1]} {fullName[0]}" : value;
            }
        }

        public string AssignmentGroup { get; set; }
        public string Opened { get; set; }
        public string OpenedBy
        {
            get => _opendBy;
            set
            {
                var fullName = value.Split(',');
                _opendBy = fullName.Length == 2
                    ? $"{fullName[1]} {fullName[0]}" : value;
            }
        }

        public string DueDate { get; set; }
        public string BreachFlag { get; set; }
        public string Closed { get; set; }
        public string ClosedBy
        {
            get => _closedBy;
            set
            {
                var fullName = value.Split(',');
                _closedBy = fullName.Length == 2
                    ? $"{fullName[1]} {fullName[0]}" : value;
            }
        }

        public string State { get; set; }
        public string ClosureCode { get; set; }
    }
}
