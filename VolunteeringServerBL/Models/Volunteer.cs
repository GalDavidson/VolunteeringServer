using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Volunteer
    {
        public Volunteer()
        {
            VolunteersInEvents = new HashSet<VolunteersInEvent>();
        }

        public int VolunteerId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int? GenderId { get; set; }
        public DateTime BirthDate { get; set; }
        public int AvgRating { get; set; }
        public DateTime? ActionDate { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
