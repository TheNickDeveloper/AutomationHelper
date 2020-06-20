using System.Collections.Generic;
using System.Configuration;

namespace AutomationHelper.Models
{
    public class TicketSourceGetter
    {
        public List<string> TicketTypes
        {
            get
            {
                return new List<string>
                {
                    "Incidents_OpsAppLasAnz",
                    "Problems_OpsAppLasAnz",
                    "Test"
                };
            }
        }

        public string Url_Incidents_OpsAppLasAnz
        {
            get => ConfigurationManager.AppSettings["Url_Incidents_OpsAppLasAnz"].ToString();
        }

        public string Url_Problems_OpsAppLasAnz
        {
            get => ConfigurationManager.AppSettings["Url_Problems_OpsAppLasAnz"].ToString(); 
        }

        public string Test
        {
            get => ConfigurationManager.AppSettings["Test"].ToString();
        }
    }
}
