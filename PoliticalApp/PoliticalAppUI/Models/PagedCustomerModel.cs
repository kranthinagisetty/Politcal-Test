using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.PoliticalAppDataEntities;
using System.Linq.Expressions;
namespace PoliticalAppUI.Models
{
   public class PagedCustomerModel
    {
        public int TotalRows { get; set; }
        public  List<Complaint> Complaint { get; set; }
        public int PageSize { get; set; }
    }

   
}
