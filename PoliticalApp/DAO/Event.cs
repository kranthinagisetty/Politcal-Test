//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server.PoliticalAppDataEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public Event()
        {
            this.Volunteers = new HashSet<Volunteer>();
        }
    
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int PoliticalPartyID { get; set; }
        public int PoliticianID { get; set; }
        public string EventLocation { get; set; }
        public System.DateTime EventBeginDate { get; set; }
        public System.DateTime EventEndDate { get; set; }
        public string Agenda { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual ICollection<Volunteer> Volunteers { get; set; }
        public virtual PoliticalParty PoliticalParty { get; set; }
        public virtual Politician Politician { get; set; }
    }
}
