using PagedList;
using Server.PoliticalAppDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticalAppUI.Models
{
    public class AppointmentsModel
    {
        public IPagedList<Appointment> appointmentList { get; set; }
        public IPagedList<Appointment> pendingAppointmentList { get; set; }
    }
}