using PoliticalAppRepository;
using PoliticalAppServiceAPI.Models;
using PoliticalAppServiceAPI.Utilities;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PoliticalAppServiceAPI.Controllers
{
     [PoliticalAuthorize]
    public class PoliticalApiController : ApiController
    {
        #region variable
        private readonly ICitizenRepository _repository;
        #endregion

        #region Constructor

        public PoliticalApiController(ICitizenRepository repository)
        {
             _repository = repository;
         }

        #endregion

        #region citizen

        // GET api/values
        [HttpGet]
        public List<CitizenModel> GetAllCitizens()
        {
           var data = HttpContext.Current.User;

           var id = ((PoliticalAppServiceAPI.Utilities.ApiPoliticalIdentity)(data.Identity)).ID; // to get politician ID

            List<CitizenModel> CitizenList = null;

          //  List<CitizenModel> listOfY = _repository.GetCitizens().Cast<CitizenModel>().ToList();            

            CitizenList = _repository.GetCitizens().Select(x => new CitizenModel
            {
                Age = x.Age,
                CitizenName = x.CitizenName,
                CitizenID = x.CitizenID
                // ...
            }).ToList();

            return CitizenList;

        }
        

        #endregion
    }
}
