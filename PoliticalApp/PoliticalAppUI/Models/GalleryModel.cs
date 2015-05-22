using PagedList;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliticalAppUI.Models
{
    public class GalleryModel
    {
        public IPagedList<Gallery> GalleryPhotoList { get; set; }
        public IPagedList<Gallery> GalleryVideoList { get; set; }
        public SelectList drpAlbumList { get; set; }
    }
}