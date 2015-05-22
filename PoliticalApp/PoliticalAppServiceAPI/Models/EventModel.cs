using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class EventModel
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int PoliticalPartyID { get; set; }
        public int PoliticianID { get; set; }
        public string EventLocation { get; set; }
        public System.DateTime EventBeginDate { get; set; }
        public System.DateTime EventEndDate { get; set; }
        public string Agenda { get; set; }
        public string Description { get; set; }
    }
}