using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class DailyEvent
    {
        public DailyEvent()
        {
            OccupationalAreasOfEvents = new HashSet<OccupationalAreasOfEvent>();
            VolunteersInEvents = new HashSet<VolunteersInEvent>();
        }

        public int EventId { get; set; }
        public string EventLocation { get; set; }
        public int? AssociationId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string Caption { get; set; }
        public string EventName { get; set; }
        public int? AreaId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Area Area { get; set; }
        public virtual Association Association { get; set; }
        public virtual ICollection<OccupationalAreasOfEvent> OccupationalAreasOfEvents { get; set; }
        public virtual ICollection<VolunteersInEvent> VolunteersInEvents { get; set; }
    }
}
