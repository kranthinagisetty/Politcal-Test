//using PoliticalAppServiceAPI.Models;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalAppRepository
{
    public interface IActivitiesRepository
    {

         List<Speech> GetAudioSpeeches();
         List<Speech> GetVideoSpeeches();        
         List<Gallery> GetPhotoAlbums();
         List<Gallery> GetVideoAlbums();
         List<Gallery> GetPictures();
         List<Gallery> GetVideos();
         List<Event> GetEvents();

         List<PartyWorker> GetPartyWorkers();
         List<Constituency> GetConstituency();
         int SubmitFeedback(FeedBack feedback);
         PoliticalParty GetPartyInformation();
         Politician GetPoliticianInformation();
         int InsertAudioVideo(Speech speech);
         int InsertGallery(Gallery gallery);
         int GetAlbumCount();
         List<Gallery> AlbumSelectByRange(int FirstPageIndex, int PageSize,int AlbumId);
         int InsertEvent(Event events);

         List<Complaint> GetComplaints();
         List<FeedBack> GeFeedBacks();
         int GetComplaintsCount();

         List<Complaint> GetComplaintsPage(int page, int pageSize, string sort, bool Dir);

         int RequestVolunteer(Volunteer volunteer);
         bool UpdateComplaint(Complaint complaint);

         bool DeleteComplaintRecord(Complaint complaint);
         Politician CheckUser(String mobileNumber, string password);

         List<Volunteer> VolunteersList();
         List<Volunteer> VolunteerRequestList();

         int RequestAppointment(Appointment appointment);


         List<Volunteer> Getvolunteers();

         List<Appointment> AppointmentList();
         List<Appointment> PendingAppointmentList();

         List<Album> GetAlbumList();

        int InsertPartyWorker(PartyWorker PartyWorker);
    }
}
