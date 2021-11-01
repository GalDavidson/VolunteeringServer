using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class OccupationalArea
    {
        public OccupationalArea()
        {
            OccupationalAreasOfAssociations = new HashSet<OccupationalAreasOfAssociation>();
        }

        public int OccupationalAreaId { get; set; }
        public string OccupationName { get; set; }

        public virtual ICollection<OccupationalAreasOfAssociation> OccupationalAreasOfAssociations { get; set; }
    }
}
