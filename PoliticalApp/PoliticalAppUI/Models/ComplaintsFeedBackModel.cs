using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.PoliticalAppDataEntities;
using PagedList;

namespace PoliticalAppUI.Models
{
    public class ComplaintsFeedBackModel
    {
        public IPagedList<Complaint> ComplaintList { get; set; }
        public IPagedList<FeedBack>  FeedBackList { get; set; }
    }
}