using Server.PoliticalAppRepository;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Data.Entity;

namespace PoliticalAppRepository
{
    public class ActivitiesRepository : RepositoryBase, IActivitiesRepository
    {
           // comments are removed by kranthi
        public List<Server.PoliticalAppDataEntities.Event> GetEvents()
        {
            List<Server.PoliticalAppDataEntities.Event> Events = null;

            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Events = dbContext.Events.ToList();
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Events);
            return Events;
        }

        public List<Server.PoliticalAppDataEntities.PartyWorker> GetPartyWorkers()
        {
            List<Server.PoliticalAppDataEntities.PartyWorker> PartyWorker = null;

            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    PartyWorker = dbContext.PartyWorkers.ToList();
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(PartyWorker);
            return PartyWorker;
        }

        

        public List<Server.PoliticalAppDataEntities.Constituency> GetConstituency()
        {
            List<Server.PoliticalAppDataEntities.Constituency> Constituencys = null;

            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Constituencys = dbContext.Constituencies.ToList();
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Constituencys);
            return Constituencys;
        }

        public int SubmitFeedback(Server.PoliticalAppDataEntities.FeedBack feedback)
        {
            //int feedbackId;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.FeedBacks.Add(feedback);
                    dbContext.SaveChanges();
                    return feedback.FeedBackID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return feedback.FeedBackID;
        }

        public List<Server.PoliticalAppDataEntities.Speech> GetAudioSpeeches()
        {
            List<Server.PoliticalAppDataEntities.Speech> Speechs = null;

            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Speechs = dbContext.Speeches.Where(x => x.IsVideo == false).ToList();
                }

                for (int i = 0; i < 1000; i++)
                {
                    Speechs.Add(new Speech { Place = "HYDERABAD" + i.ToString(), CreatedDate = DateTime.Now, SpeechName = "Polspeech" + i.ToString() });
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Speechs);
            return Speechs;
        }

        public List<Server.PoliticalAppDataEntities.Speech> GetVideoSpeeches()
        {
            List<Server.PoliticalAppDataEntities.Speech> Speechs = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Speechs = dbContext.Speeches.Where(x => x.IsVideo == true).ToList();
                }
                for (int i = 0; i < 1000; i++)
                {
                    Speechs.Add(new Speech { Place = "HYDERABAD" + i.ToString(), CreatedDate = DateTime.Now, SpeechName = "Polspeech" + i.ToString() });
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Speechs);
            return Speechs;
        }

        public List<Server.PoliticalAppDataEntities.Gallery> GetPhotoAlbums()
        {
            List<Server.PoliticalAppDataEntities.Gallery> Galleries = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {

                    Galleries = dbContext.Galleries.ToList();
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Galleries);
            return Galleries;
        }

        public List<Server.PoliticalAppDataEntities.Gallery> GetVideoAlbums()
        {
            List<Server.PoliticalAppDataEntities.Gallery> Galleries = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Galleries = dbContext.Galleries.Where(x => x.IsVideo == false).ToList();
                }

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Galleries);
            return Galleries;
        }

        public PoliticalParty GetPartyInformation()
        {
            Server.PoliticalAppDataEntities.PoliticalParty PoliticalPary = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    PoliticalPary = dbContext.PoliticalParties.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(PoliticalPary);
            return PoliticalPary;
        }

        public Politician GetPoliticianInformation()
        {
            Server.PoliticalAppDataEntities.Politician Politician = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Politician = dbContext.Politicians.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Politician);
            return Politician;
        }


        public int InsertAudioVideo(Speech speech)
        {
            int SpeechID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Speeches.Add(speech);
                    dbContext.SaveChanges();
                    return speech.SpeechID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return SpeechID;
        }

        public int InsertGallery(Gallery Gallery)
        {
            int GalleryId = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Galleries.Add(Gallery);
                    dbContext.SaveChanges();
                    return Gallery.GalleryID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return GalleryId;

        }

        public int GetAlbumCount()
        {
            int citizens = 0;
            try
            {
                citizens = this.GetPhotoAlbums().Count();
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(citizens);
            return citizens;
        }

        public List<Gallery> AlbumSelectByRange(int FirstPageIndex, int PageSize, int AlbumId = 0)
        {
            List<Gallery> gallery = null;
            try
            {
                if (AlbumId != -1)
                    gallery = this.GetPhotoAlbums().Skip(FirstPageIndex - 1).Take(PageSize - (FirstPageIndex - 1)).Where(x => x.AlbumId == AlbumId).ToList();
                else
                    gallery = this.GetPhotoAlbums().Skip(FirstPageIndex - 1).Take(PageSize - (FirstPageIndex - 1)).ToList();

            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(gallery);
            return gallery;

        }


        public int InsertEvent(Event events)
        {
            int EventID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Events.Add(events);
                    dbContext.SaveChanges();
                    return events.EventID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return EventID;
        }


        public int InsertPartyWorker(PartyWorker PartyWorker)
        {
            int PartyWorkerID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.PartyWorkers.Add(PartyWorker);
                    dbContext.SaveChanges();
                    return PartyWorker.PartyWokerID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return PartyWorkerID;
        }


        public List<Complaint> GetComplaints()
        {
            List<Complaint> complaint = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    complaint = dbContext.Complaints.ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(complaint);
            return complaint;

        }

        public List<FeedBack> GetFeedBack()
        {
            List<FeedBack> feedback = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    feedback = dbContext.FeedBacks.ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(feedback);
            return feedback;

        }


        public List<Gallery> GetPictures()
        {
            List<Gallery> Pictures = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Pictures = dbContext.Galleries.Where(x => x.IsVideo == false).ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(Pictures);
            return Pictures;

        }

        public List<Gallery> GetVideos()
        {
            List<Gallery> videos = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    videos = dbContext.Galleries.Where(x => x.IsVideo == true).ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(videos);
            return videos;

        }

        public int GetComplaintsCount()
        {
            int complaintCount = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    complaintCount = this.GetComplaints().Count();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return complaintCount;

        }


        public List<Complaint> GetComplaintsPage(int pageNumber, int pageSize, string sort, bool Dir)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (sort == "Subject")
                return this.GetComplaints().OrderByDescending(x => x.Subject)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToList();
            else if (sort == "AddedOn")
                return this.GetComplaints().OrderByDescending(x => x.AddedOn)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToList();
            else if (sort == "CitizenID")
                return this.GetComplaints().OrderByDescending(x => x.CitizenID)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToList();
            else
                return this.GetComplaints()
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToList();
        }



        public int RequestVolunteer(Volunteer volunteer)
        {
            int VolunteerID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Volunteers.Add(volunteer);
                    dbContext.SaveChanges();
                    return volunteer.VolunteerID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return VolunteerID;
        }

        public bool UpdateComplaint(Complaint complaint)
        {
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Complaint _complaint = dbContext.Complaints.Where(x => x.ComplaintID == complaint.ComplaintID).FirstOrDefault();

                    _complaint.ComplaintID = complaint.ComplaintID;
                    _complaint.Subject = complaint.Subject;
                    _complaint.AddedOn = complaint.AddedOn;
                    _complaint.Description = complaint.Description;
                    _complaint.Status = 1;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
                return false;
            }
        }

        public bool DeleteComplaintRecord(Complaint complaint)
        {
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    Complaint _complaint = dbContext.Complaints.Where(x => x.ComplaintID == complaint.ComplaintID).FirstOrDefault();
                    dbContext.Complaints.Remove(_complaint);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public Politician CheckUser(string mobileNumber, string password)
        {
            Politician _Politician = new Politician();

            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    return dbContext.Politicians.Where(x => x.MoblieNumber == mobileNumber && x.Password == password).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

            }
            return _Politician;
        }


        public List<Volunteer> GetNewVolunteer()
        {
            List<Volunteer> volunteer = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    volunteer = dbContext.Volunteers.Where(x => x.Status == false).ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(volunteer);
            return volunteer;
        }




        public List<Volunteer> VolunteersList()
        {
            List<Volunteer> volunteer = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    volunteer = dbContext.Volunteers.Where(x => x.Status == true).ToList();
                }

                for (int i = 0; i < 1000; i++)
                {


                    volunteer.Add(new Volunteer
                    {
                        EventID = i,
                        CitizenID = i,
                        AddedOn = DateTime.UtcNow


                    });
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(volunteer);
            return volunteer;
        }

        public List<Volunteer> VolunteerRequestList()
        {
            List<Volunteer> volunteer = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    volunteer = dbContext.Volunteers.ToList().Where(x => x.Status == false).ToList();
                }

                for (int i = 0; i < 1000; i++)
                {


                    volunteer.Add(new Volunteer
                    {
                        EventID = i,
                        CitizenID = i,
                        AddedOn = DateTime.UtcNow

                    });
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(volunteer);
            return volunteer;
        }





        public List<FeedBack> GeFeedBacks()
        {
            List<FeedBack> feedback = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    feedback = dbContext.FeedBacks.ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(feedback);
            return feedback;
        }


        public int RequestAppointment(Appointment appointment)
        {
            int AppointmentID = 0;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    dbContext.Appointments.Add(appointment);
                    dbContext.SaveChanges();
                    return appointment.AppointmentID;
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            return AppointmentID;
        }


        public List<Appointment> AppointmentList()
        {
            List<Appointment> appointmentList = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    appointmentList = dbContext.Appointments.Where(x => x.AppointmentStatusID == 1).ToList();
                }

                for (int i = 0; i < 4000; i++)
                {
                    appointmentList.Add(new Appointment { AppointmentDate = DateTime.Now, CitizenID = i, Message = "message" + i.ToString() });
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(appointmentList);
            return appointmentList;
        }

        public List<Appointment> PendingAppointmentList()
        {
            List<Appointment> appointmentList = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    appointmentList = dbContext.Appointments.Where(x => x.AppointmentStatusID == 0).ToList();
                }


                for (int i = 0; i < 4000; i++)
                {
                    appointmentList.Add(new Appointment { AppointmentDate = DateTime.Now, CitizenID = i, Message = "message" + i.ToString() });
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(appointmentList);
            return appointmentList;
        }


        public List<Volunteer> Getvolunteers()
        {
            List<Volunteer> volunteer = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    volunteer = dbContext.Volunteers.ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(volunteer);
            return volunteer;
        }


        public List<Album> GetAlbumList()
        {
            List<Album> AlbumList = null;
            try
            {
                using (PoliticalAppDBContext dbContext = new PoliticalAppDBContext())
                {
                    AlbumList = dbContext.Albums.ToList();
                }
            }
            catch (Exception ex)
            {
                HandleServerException(ex);
            }
            HandleNullException(AlbumList);
            return AlbumList;
        }
    }
}
