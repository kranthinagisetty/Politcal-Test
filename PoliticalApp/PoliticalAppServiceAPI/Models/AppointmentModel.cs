using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class AppointmentModel
    {
        public int AppointmentID { get; set; }       
        public string Message { get; set; }
        public byte AppointmentStatusID { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}