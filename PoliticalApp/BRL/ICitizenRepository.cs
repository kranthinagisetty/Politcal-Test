using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalAppRepository
{
    public interface ICitizenRepository
    {
         Citizen GetCitizen(int citizenID);
         List<Citizen> GetCitizens();
         int GetCitizensCount();
         List<Citizen>  CitizenSelectByRange(int FirstPageIndex, int PageSize);
         int AddCitizen(Citizen citizen);
         int UpdateCitizen(Citizen citizen);
   
    }
}
