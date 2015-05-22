using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppServiceAPI.Models
{
    public class GalleryModel
    {
        public Nullable<int> AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public string AlbumLink { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
   
     
    }
}