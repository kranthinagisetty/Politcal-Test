using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class FeedBackModel
    {
       // public int FeedBackId { get; set; }
        public int CitizenID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}