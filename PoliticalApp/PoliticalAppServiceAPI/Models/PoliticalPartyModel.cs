using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class PoliticalPartyModel
    {
        public string PartyName { get; set; }
        public DateTime FoundedOn { get; set; }
        public string President { get; set; }
        public string Address { get; set; }
        public string History { get; set; }
        public List<ConstituencyModel> ConstituencyData { get; set; }
       
    }
}