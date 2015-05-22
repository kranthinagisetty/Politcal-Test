using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class SpeechModel
    {
        public short SpeechID { get; set; }
        public string SpeechName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string SpeechDescription { get; set; }
        public string SpeechLink { get; set; }
        public bool IsVideo { get; set; }
        public string Place { get; set; }
    }
}