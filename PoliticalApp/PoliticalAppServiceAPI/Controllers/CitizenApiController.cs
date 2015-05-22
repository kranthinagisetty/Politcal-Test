using PoliticalAppRepository;
using PoliticalAppServiceAPI.Utilities;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoliticalAppServiceAPI.Models;
using System.IO;
using System.Web;
namespace PoliticalAppServiceAPI.Controllers
{
    [CitizenAuthorize]
    public class CitizenApiController : ApiController
    {
        #region variable
        private readonly ICitizenRepository _repository;
        private readonly IActivitiesRepository _actRepository;
        private PoliticalAppDBContext database = new PoliticalAppDBContext();

        //private const int PageSize = 1;
        //private const int FirstPageIndex = 1;
        private int? _fullAlbumCount;
        #endregion

        #region Constructor

        public CitizenApiController(ICitizenRepository repository, IActivitiesRepository actRepository)
        {
            _repository = repository;
            _actRepository = actRepository;
        }

        #endregion

        #region Public Properties

        public int FullAlbumCount
        {
            get
            {
                if (_fullAlbumCount == null)
                {
                    _fullAlbumCount = _actRepository.GetAlbumCount();
                }

                return (int)_fullAlbumCount;
            }
        }

        #endregion

        #region Citizen

        [HttpGet]
        public Citizen GetCitizen(int id)
        {
            return _repository.GetCitizen(id);
        }


        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage RegisterCitizen(CitizenModel citizen)
        {
            int _record = 0;

            var data = HttpContext.Current.User;
            var id = ((PoliticalAppServiceAPI.Utilities.ApiPoliticalIdentity)(data.Identity)).ID; // to get Citizen ID

            if (citizen == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                Citizen _citizen = new Citizen
                {
                    CitizenName = citizen.CitizenName,
                    CitizenID = id,
                    ConstituencyID = citizen.ConstituencyID,
                    MobileNumber = citizen.MobileNumber,
                    Email = citizen.Email,
                    Gender = citizen.Gender,
                    Age = citizen.Age,
                    Address = citizen.Address,
                    Description = citizen.Description,
                    CitizenType = citizen.CitizenType,
                    Password = citizen.Password,
                    ConfirmPassword = citizen.ConfirmPassword
                };
                _record = _repository.AddCitizen(_citizen);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _record);
        }

        [HttpPost]
        public HttpResponseMessage UpdateCitizen(Citizen citizen)
        {

            int _record = 0;

            if (citizen == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                _record = _repository.UpdateCitizen(citizen);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _record);
        }

        #endregion

        #region Speeches, Galleries, Albums, Events,Feedback

        [HttpGet]
        public List<SpeechModel> GetAudioSpeeches()
        {
            List<SpeechModel> SpeechList = null;

            SpeechList = _actRepository.GetAudioSpeeches().Select(x => new SpeechModel
            {
                SpeechName = x.SpeechName,
                SpeechDescription = x.SpeechDescription,
                SpeechLink = "http://qa-politicalappcms.azurewebsites.net" + x.SpeechLink.Replace("~", "")

            }).ToList();

            return SpeechList;
        }

        [HttpGet]
        public List<SpeechModel> GetVideoSpeeches()
        {

            List<SpeechModel> SpeechList = null;

            SpeechList = _actRepository.GetVideoSpeeches().Select(x => new SpeechModel
            {
                SpeechName = x.SpeechName,
                SpeechDescription = x.SpeechDescription,
                SpeechLink = "http://qa-politicalappcms.azurewebsites.net" + x.SpeechLink.Replace("~", "")

            }).ToList();

            return SpeechList;


        }

        [HttpGet]
        public List<Event> GetEvents()
        {
            return _actRepository.GetEvents();

        }


        [HttpGet]
        public List<GalleryModel> GetPhotoAlbums(int FirstPageIndex, int Pagesize, int AlbumId)
        {
            List<GalleryModel> gallery = new List<GalleryModel>();
            gallery = _actRepository.AlbumSelectByRange(FirstPageIndex, Pagesize, AlbumId).Select(x => new GalleryModel
            {

                AlbumTitle = x.AlbumName,
                CreatedDate = DateTime.UtcNow,
                AlbumLink = x.AlbumLink,
                AlbumId = x.AlbumId
            }).ToList();
            return gallery;
        }

        [HttpGet]
        public List<Gallery> GetVideoAlbums()
        {
            return _actRepository.GetVideoAlbums();

        }

        [HttpGet]
        public List<GalleryModel> GetPictures(int albumId)
        {
            List<GalleryModel> PicturesList = null;

            PicturesList = _actRepository.GetPictures().Select(x => new GalleryModel
            {
                AlbumLink = x.AlbumLink,
            }).ToList();

            return PicturesList;

        }

        [HttpGet]
        public List<GalleryModel> GetVideos(int albumId)
        {
            List<GalleryModel> VideoList = null;

            VideoList = _actRepository.GetVideos().Select(x => new GalleryModel
            {
                AlbumLink = x.AlbumLink,
            }).ToList();

            return VideoList;

        }

        [HttpPost]
        public HttpResponseMessage SubmitFeedback(FeedBackModel feedback)
        {
            int _record = 0;

            if (feedback == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                FeedBack _feedback = new FeedBack
                {
                    Type = feedback.Type,
                    CitizenID = feedback.CitizenID,
                    Subject = "",
                    Description = feedback.Description
                };

                _record = _actRepository.SubmitFeedback(_feedback);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _record);
        }


        #endregion

        #region Appointments, Volunteers

        [HttpGet]
        public List<VolunteerModel> GetVolunteers()
        {
            List<Volunteer> VolunteerList = null;
            //List<VolunteerModel> VolunteerModelList = null;
            List<Citizen> citizenList = null;
            List<Event> eventList = null;
           
            VolunteerList = _actRepository.Getvolunteers();
            citizenList = _repository.GetCitizens();
            eventList = _actRepository.GetEvents();

            var VolunteerModelList = VolunteerList
            .Join(citizenList, p => p.CitizenID, pc => pc.CitizenID, (p, pc) => new { p, pc })
            .Join(eventList, ppc => ppc.p.EventID, c => c.EventID, (ppc, c) => new VolunteerModel
            {
                CitizenID = ppc.p.CitizenID,
                Name = ppc.pc.Address,
                EventID = ppc.p.EventID,
               // EventName = ppc.p.Event.EventName
            }).ToList();
            return VolunteerModelList;
        }

        [HttpPost]
        public HttpResponseMessage RequestAppointment(AppointmentModel appointment)
        {
            int _record = 0;

            if (appointment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                var data = HttpContext.Current.User;
                var id = Convert.ToInt32(((PoliticalAppServiceAPI.Utilities.ApiCitizenIdentity)(data.Identity)).ID); // to get Citizen ID

                Appointment _appointmentr = new Appointment();
                _appointmentr.CitizenID = id;
                _appointmentr.AppointmentDate = appointment.AppointmentDate;
                _appointmentr.UpdatedOn = DateTime.Now;
                _appointmentr.AddedOn = DateTime.Now;
                _appointmentr.Message = appointment.Message;
                _appointmentr.AppointmentStatusID = 0;

                _record = _actRepository.RequestAppointment(_appointmentr);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _record);
        }

        [HttpPost]
        public HttpResponseMessage RequestVolunteers(VolunteerModel volunteers)
        {
            int _record = 0;

            if (volunteers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var data = HttpContext.Current.User;
            var id = Convert.ToInt32(((PoliticalAppServiceAPI.Utilities.ApiCitizenIdentity)(data.Identity)).ID); // to get Citizen ID

            Volunteer _Volunteer = new Volunteer();
            _Volunteer.CitizenID = id;
            _Volunteer.EventID = volunteers.EventID;
            _Volunteer.UpdatedOn = DateTime.Now;
            _Volunteer.AddedOn = DateTime.Now;
            _Volunteer.Status = false;

            _record = _actRepository.RequestVolunteer(_Volunteer);


            return Request.CreateResponse(HttpStatusCode.OK, _record);
        }

        [HttpPost]
        public HttpResponseMessage RequestPartyMember(VolunteerModel volunteers)
        {
            int _record = 0;

            if (volunteers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var data = HttpContext.Current.User;
            var id = Convert.ToInt32(((PoliticalAppServiceAPI.Utilities.ApiCitizenIdentity)(data.Identity)).ID); // to get Citizen ID

            Volunteer _Volunteer = new Volunteer();
            _Volunteer.CitizenID = id;
            _Volunteer.EventID = volunteers.EventID;
            _Volunteer.UpdatedOn = DateTime.Now;
            _Volunteer.AddedOn = DateTime.Now;
            _Volunteer.Status = false;

            _record = _actRepository.RequestVolunteer(_Volunteer);


            return Request.CreateResponse(HttpStatusCode.OK, _record);
        }


        #endregion

        #region Political Party information
        [HttpGet]
        public PoliticalPartyModel GetPartyInformation()
        {
            PoliticalPartyModel politicalPartyList = new PoliticalPartyModel();

            var dbPoliticalParty = _actRepository.GetPartyInformation();

            if (dbPoliticalParty != null)
            {
                politicalPartyList.PartyName = dbPoliticalParty.PartyName;
                politicalPartyList.Address = dbPoliticalParty.Address;
                politicalPartyList.President = dbPoliticalParty.President;
                politicalPartyList.FoundedOn = dbPoliticalParty.FoundedOn;
                politicalPartyList.History = dbPoliticalParty.History;
            }
            return politicalPartyList;
        }

        #endregion

        #region Politician Information
        [HttpGet]
        public PoliticianModel GetPoliticianInformation()
        {
            PoliticianModel PoliticianList = new PoliticianModel();

            var dbPolitician = _actRepository.GetPoliticianInformation();
            if (dbPolitician != null)
            {
                PoliticianList.PoliticianName = dbPolitician.PoliticianName;
                PoliticianList.PoliticanProfileUrl = dbPolitician.PoliticanProfileUrl;
                PoliticianList.Description = dbPolitician.Description;
                PoliticianList.Address = dbPolitician.Address;
            }
            return PoliticianList;
        }
        #endregion

        #region Get constituency Details
        [HttpGet]
        public PoliticalPartyModel GetconstituencyDetails()
        {

            PoliticalPartyModel PoliticalPartyList = new PoliticalPartyModel(); ;
            var dbPoliticalParty = _actRepository.GetPartyInformation();
            PoliticalPartyList.ConstituencyData = new List<ConstituencyModel>();
            PoliticalPartyList.PartyName = dbPoliticalParty.PartyName;
            PoliticalPartyList.ConstituencyData = _actRepository.GetConstituency().Select(x => new ConstituencyModel
              {
                  ConstituencyID = x.ConstituencyID,
                  ConsituencyName = x.ConsituencyName
              }).ToList();
            return PoliticalPartyList;
        }
        #endregion



    }
}