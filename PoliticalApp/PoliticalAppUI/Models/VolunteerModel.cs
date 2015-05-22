using PagedList;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppUI.Models
{
    public class VolunteerModel
    {
        public IPagedList<Volunteer> VolunteersList { get; set; }
        public IPagedList<Volunteer> VolunteerRequestList { get; set; }
    }
}