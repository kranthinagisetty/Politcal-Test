using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class ComplaintModel
    {
        public int ComplaintID { get; set; }
        public int CitizenID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }
    }
}