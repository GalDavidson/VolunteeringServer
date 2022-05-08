using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Area
    {
        public Area()
        {
            DailyEvents = new HashSet<DailyEvent>();
        }

        public int AreaId { get; set; }
        public string AreaName { get; set; }

        public virtual ICollection<DailyEvent> DailyEvents { get; set; }
    }
}
