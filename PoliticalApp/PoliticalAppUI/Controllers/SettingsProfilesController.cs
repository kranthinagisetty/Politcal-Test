using PoliticalAppRepository;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using PagedList;
using PoliticalAppUI.Models;
namespace PoliticalAppUI.Controllers
{
    public class SettingsProfilesController : BaseController
    {
        //
        // GET: /SettingsProfiles/

        #region Private Members

        private IActivitiesRepository _ActRepository;

        #endregion

        #region Constructor
        public SettingsProfilesController(IActivitiesRepository ActRepository)
        {
            _ActRepository = ActRepository;
        }
        #endregion

        public ActionResult Index(int page = 1)
        {
            PartyworkerModel _partyWorkerModel = new PartyworkerModel();
            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }
            _partyWorkerModel.PartyWorkersList = _ActRepository.GetPartyWorkers().ToPagedList(page, 5);
            return View(_partyWorkerModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(PartyWorker partyWorker)
        {
            partyWorker.CitizenID = 5;

            partyWorker.PoliticianID = _ActRepository.GetPartyInformation().PoliticalPartyID;

            Politician pol = (Politician)Session["User"];
            partyWorker.PoliticianID = pol.PoliticianID;
            var id = _ActRepository.InsertPartyWorker(partyWorker);

            return RedirectToAction("Index");
        }

        public ActionResult Profiles()
        {
            return View();
        }

    }
}
