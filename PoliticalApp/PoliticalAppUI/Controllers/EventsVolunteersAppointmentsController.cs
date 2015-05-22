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
    public class EventsVolunteersAppointmentsController : BaseController
    {

        #region Private Members

        private IActivitiesRepository _ActRepository;

        #endregion

        #region Constructor
        public EventsVolunteersAppointmentsController(IActivitiesRepository ActRepository)
        {
            _ActRepository = ActRepository;
        }
        #endregion

        #region Action Methods

        // GET: EventsVolunteersAppointments
        public ActionResult Index(int page = 1)
        {
            EventsModel _eventsModel = new EventsModel();
            // I do this just in case someone tries to put in 0 or a negative number
            if (page < 1)
            {
                page = 1;
            }
             _eventsModel.eventsList = _ActRepository.GetEvents().ToPagedList(page, 5);


             return View(_eventsModel);
        }

       [System.Web.Mvc.HttpPost]
        public ActionResult Index(Event events)
        {
            events.EventBeginDate = DateTime.UtcNow;
            events.EventEndDate = DateTime.UtcNow;
            events.AddedOn = DateTime.UtcNow;
            events.UpdatedOn = DateTime.UtcNow;
            events.PoliticalPartyID = _ActRepository.GetPartyInformation().PoliticalPartyID;
            Politician pol = (Politician)Session["User"];

            events.PoliticianID = pol.PoliticianID;
            var id = _ActRepository.InsertEvent(events);

            return RedirectToAction("Index");
        }

       public ActionResult Volunteers(int page = 1, int page1 = 1)
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

            VolunteerModel volunteers = new VolunteerModel();

            int pageSize = 5;

            //Used the following two formulas so that it doesn't round down on the returned integer
            decimal VolunteerListTotalPages = ((decimal)(_ActRepository.VolunteersList().Count() / (decimal)pageSize));
            decimal VolunterRequestTotalPages = ((decimal)(_ActRepository.VolunteerRequestList().Count() / (decimal)pageSize));

            volunteers.VolunteersList = _ActRepository.VolunteersList().ToPagedList(page, pageSize);
            volunteers.VolunteerRequestList = _ActRepository.VolunteerRequestList().ToPagedList(page1, pageSize);

            ViewBag.VolunteerListTotalPages = Math.Ceiling(VolunteerListTotalPages);
            ViewBag.VolunteerRequestTotalPages = Math.Ceiling(VolunterRequestTotalPages);

            ViewBag.PageNumberAudio = page;
            ViewBag.PageNumberVideo = page1;

            if (page > 1)
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("VolunteerPartialList", volunteers) : View(volunteers);
            else
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("VolunteerPartialRequest", volunteers) : View(volunteers);

        }

        public ActionResult Appointments(int page = 1, int page1 = 1)
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

            AppointmentsModel Appointments = new AppointmentsModel();

            int pageSize = 5;

            //Used the following two formulas so that it doesn't round down on the returned integer
            decimal AppointmentsTotalPages = ((decimal)(_ActRepository.AppointmentList().Count() / (decimal)pageSize));
            decimal PendingAppointmentsTotalPages = ((decimal)(_ActRepository.PendingAppointmentList().Count() / (decimal)pageSize));

            Appointments.appointmentList = _ActRepository.AppointmentList().ToPagedList(page, pageSize);
            Appointments.pendingAppointmentList = _ActRepository.PendingAppointmentList().ToPagedList(page1, pageSize);

            ViewBag.AppointmentsTotalPages = Math.Ceiling(AppointmentsTotalPages);
            ViewBag.PendingAppointmentsTotalPages = Math.Ceiling(PendingAppointmentsTotalPages);

            ViewBag.PageNumberAudio = page;
            ViewBag.PageNumberVideo = page1;

            if (page > 1)
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("AppointmentsPartial", Appointments) : View(Appointments);
            else
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("PendingAppointmentsPartial", Appointments) : View(Appointments);

        }
        #endregion
    }
}
