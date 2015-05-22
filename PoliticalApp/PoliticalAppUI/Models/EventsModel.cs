using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.PoliticalAppDataEntities;
using PagedList;

namespace PoliticalAppUI.Models
{
    public class EventsModel
    {        
        public Event events { get; set; }
        public IPagedList<Event> eventsList { get; set; }
    }
}