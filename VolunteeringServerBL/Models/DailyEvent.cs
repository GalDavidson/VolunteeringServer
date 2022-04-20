using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class DailyEvent
    {
        public DailyEvent()
        {
            VolunteersInEvents = new HashSet<VolunteersInEvent>();
        }

        public int EventId { get; set; }
        public string EventLocation { get; set; }
        public int? AssociationId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }

        public virtual Association Association { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
