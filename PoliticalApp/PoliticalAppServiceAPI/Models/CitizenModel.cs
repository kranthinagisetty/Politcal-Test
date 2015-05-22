using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class CitizenModel
    {
        public int    CitizenID { get; set; }
        public int    ConstituencyID { get; set; }
        public string CitizenName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public byte   Gender { get; set; }
        public byte   Age { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public byte   CitizenType { get; set; }       
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}