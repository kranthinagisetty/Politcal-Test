using PoliticalAppRepository;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliticalAppUI.Controllers
{
    public class LoginController : Controller
    {
        #region private member
        private IActivitiesRepository _ActRepository;
        #endregion

        #region Constructor
        public LoginController(IActivitiesRepository ActRepository)
        {
            _ActRepository = ActRepository;
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String mobileNumber, string password)
        {

            Politician politician = _ActRepository.CheckUser(mobileNumber, password);


            if (politician != null)
            {
                Session["User"] = politician;
                Session["UserName"] = politician.PoliticianName.ToString();
                return RedirectToAction("Index", "Citizen");
            }
            else
            {
                Session["UserName"] = null;
                return RedirectToAction("Index", "Login");
            }

        }

    }
}
