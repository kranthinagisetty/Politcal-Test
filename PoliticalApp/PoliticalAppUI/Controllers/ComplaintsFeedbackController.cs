using PoliticalAppRepository;
using PoliticalAppUI.Models;
using Server.PoliticalAppDataEntities;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.IO;
using PagedList;
namespace PoliticalAppUI.Controllers
{
    public class ComplaintsFeedbackController : BaseController
    {

        #region private member
        private IActivitiesRepository _ActRepository;
        #endregion

        #region Constructor
        public ComplaintsFeedbackController(IActivitiesRepository ActRepository)
        {
            _ActRepository = ActRepository;
        }
        #endregion

        #region ActionMethods

        //public ActionResult Index(int page = 1, int sortBy = 1, bool isAsc = true, string search = null)
        //{
        //    List<Complaint> Complaint = new List<Complaint>();
        //    const int pageSize = 5;
        //    var totalRows = _ActRepository.GetComplaintsCount();

        //    Complaint = _ActRepository.GetComplaintsPage(page, pageSize, "citizenId", true);


        //    var data = new PagedCustomerModel()
        //    {
        //        TotalRows = totalRows,
        //        PageSize = pageSize,
        //        Complaint = Complaint
        //    };
        //    return View(data);
        //}

        public ActionResult Index(int page = 1, int page1 = 1)
        {
            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }

            if (page1 < 1)
            {
                page1 = 1;
            }

            ComplaintsFeedBackModel CompliantFeedBacks = new ComplaintsFeedBackModel();

            int pageSize = 5;

            //Used the following two formulas so that it doesn't round down on the returned integer
            decimal ComplainttotalPages = ((decimal)(_ActRepository.GetComplaints().Count() / (decimal)pageSize));
            decimal FeedBacktotalPages = ((decimal)(_ActRepository.GeFeedBacks().Count() / (decimal)pageSize));

            CompliantFeedBacks.ComplaintList = _ActRepository.GetComplaints().ToPagedList(page, pageSize);
            CompliantFeedBacks.FeedBackList = _ActRepository.GeFeedBacks().ToPagedList(page1, pageSize);

            ViewBag.ComplainttotalPages = Math.Ceiling(ComplainttotalPages);
            ViewBag.FeedBacktotalPages = Math.Ceiling(FeedBacktotalPages);

            ViewBag.pageNumberComplaint = page;
            ViewBag.PageNumberFeedBack = page1;
            if (page > 1)
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("ComplaintsPartialList", CompliantFeedBacks) : View(CompliantFeedBacks);
            else
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("FeedbackPartialList", CompliantFeedBacks) : View(CompliantFeedBacks);

        }
        public ActionResult Feedback()
        {
            return View();
        }

       
        #endregion



    }
}
