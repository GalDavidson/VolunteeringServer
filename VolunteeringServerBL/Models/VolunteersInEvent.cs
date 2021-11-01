using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class VolunteersInEvent
    {
        public VolunteersInEvent()
        {
            Comments = new HashSet<Comment>();
        }

        public int EventId { get; set; }
        public int VolunteerId { get; set; }
        public int RatingNum { get; set; }
        public string WrittenRating { get; set; }
        public DateTime? ActionDate { get; set; }

        public virtual DailyEvent Event { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
