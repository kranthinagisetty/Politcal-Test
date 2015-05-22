using Server.PoliticalAppRepository;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace PoliticalAppRepository
{
    public class CitizenRepository : RepositoryBase, ICitizenRepository
    {
        
        #region Variables
       
        #endregion Variables

        #region Constructor
        public CitizenRepository()
        {
           
        }
        #endregion

        #region Get citizen Methods Section

        public Citizen GetCitizen(int citizenID)
        {
            Citizen citizen = null;
            try
            {
                 using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    return dbContext.Citizens.Include("Appointments").Where(p => p.CitizenID == citizenID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(citizen);
            return citizen;
        }

        public List<Citizen> GetCitizens()
        {
            List<Citizen> citizens = null;

           

            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {

                    dbContext.Configuration.ProxyCreationEnabled = false;

                    citizens = dbContext.Citizens.ToList();
                }

            }


            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(citizens);
            return citizens;
        }

        public int GetCitizensCount()
        {
            int citizens = 0;
            try
            {
                citizens = this.GetCitizens().Count();
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(citizens);
            return citizens;
        }

        public List<Citizen> CitizenSelectByRange(int FirstPageIndex, int PageSize)
        {
            List<Citizen> citizens = null;
            try
            {
                citizens = this.GetCitizens().Skip(FirstPageIndex - 1).Take(PageSize - (FirstPageIndex - 1)).ToList();
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(citizens);
            return citizens;

        }

        public int AddCitizen(Citizen citizen)
        {
            int citizenID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Citizens.Add(citizen);
                    dbContext.SaveChanges();
                    return citizen.CitizenID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return citizenID;
        }

        public int UpdateCitizen(Citizen citizen)
        {
            int citizenID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Citizens.Add(citizen);
                    dbContext.Entry(citizen).State = EntityState.Modified;
                    return citizen.CitizenID;

                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return citizenID;
        }
    
        #endregion





       
    }
}
