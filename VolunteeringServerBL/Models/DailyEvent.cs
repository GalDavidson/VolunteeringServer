using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class DailyEvent
    {
        public DailyEvent()
        {
            OccupationalAreasOfPosts = new HashSet<OccupationalAreasOfPost>();
            PicturesOfEvents = new HashSet<PicturesOfEvent>();
            Posts = new HashSet<Post>();
            VolunteersInEvents = new HashSet<VolunteersInEvent>();
        }

        public int EventId { get; set; }
        public string EventLocation { get; set; }
        public string Caption { get; set; }
        public int? AssociationId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }

        public virtual Association Association { get; set; }
        public virtual ICollection<OccupationalAreasOfPost> OccupationalAreasOfPosts { get; set; }
        public virtual ICollection<PicturesOfEvent> PicturesOfEvents { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
