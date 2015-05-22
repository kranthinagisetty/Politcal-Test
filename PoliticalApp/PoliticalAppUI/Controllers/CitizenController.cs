using PoliticalAppRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PoliticalAppUI.Controllers
{
    public class CitizenController : BaseController
    {

        #region Private Members

        private ICitizenRepository _citizenRepository;

        private const int PageSize = 5;
        private const int FirstPageIndex = 1;
        private int? _fullCategoryCount;

        #endregion

        public CitizenController(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }

        #region Public Properties

        public int FullCategoryCount
        {
            get
            {
                if (_fullCategoryCount == null)
                {
                    _fullCategoryCount = _citizenRepository.GetCitizensCount();
                }

                return (int)_fullCategoryCount;
            }
        }

        #endregion

        #region Action Methods

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        

       

        #endregion

        #region Private Methods

       

        #endregion


    }
}
