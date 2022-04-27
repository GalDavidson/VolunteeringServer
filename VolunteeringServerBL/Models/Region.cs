using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Region
    {
        public Region()
        {
            DailyEvents = new HashSet<DailyEvent>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<DailyEvent> DailyEvents { get; set; }
    }
}
