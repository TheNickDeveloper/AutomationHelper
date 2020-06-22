namespace AutomationHelper.Models
{
    public class IncidentResultTable : IResultTable
    {
        private string _client = string.Empty;
        private string _opendBy = string.Empty;
        private string _assignedTo = string.Empty;
        private string _shortDescription = string.Empty;

        public string Number { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public string Client
        {
            get => _client;
            set
            {
                var fullName = value.Split(',');
                _client = fullName.Length == 2
                    ? $"{fullName[1]} {fullName[0]}" : value;
            }
        }

        public string ShortDescription
        {
            get => _shortDescription;
            set
            {
                value = value.Replace(',', '.');
            }
        }

        public string IncidentDueDate { get; set; }
        public string AssignTo
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

    }
}
