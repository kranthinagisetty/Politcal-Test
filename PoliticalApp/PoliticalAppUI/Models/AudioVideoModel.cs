using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.PoliticalAppDataEntities;
using PagedList;

namespace PoliticalAppUI.Models
{
    public class AudioVideoModel
    {
        public IPagedList<Speech> audioList { get; set; }
        public IPagedList<Speech> videoList { get; set; }
    }
}