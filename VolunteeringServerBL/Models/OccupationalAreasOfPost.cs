using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class OccupationalAreasOfPost
    {
        public int PostId { get; set; }
        public int OccupationalAreaId { get; set; }

        public virtual DailyEvent OccupationalArea { get; set; }
        public virtual OccupationalArea OccupationalAreaNavigation { get; set; }
    }
}
