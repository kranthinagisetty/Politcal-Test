using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.PoliticalAppDataEntities;
using PagedList;


namespace PoliticalAppUI.Models
{
    public class PartyworkerModel
    {
        public PartyWorker  PartyWorkers { get; set; }
        public IPagedList<PartyWorker> PartyWorkersList { get; set; }
    }
}