using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Association
    {
        public Association()
        {
            BranchesOfAssociations = new HashSet<BranchesOfAssociation>();
            DailyEvents = new HashSet<DailyEvent>();
            OccupationalAreasOfAssociations = new HashSet<OccupationalAreasOfAssociation>();
            Posts = new HashSet<Post>();
        }

        public int AssociationId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string InformationAbout { get; set; }
        public string PhoneNum { get; set; }
        public string Pass { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ProfilePic { get; set; }

        public virtual ICollection<BranchesOfAssociation> BranchesOfAssociations { get; set; }
        public virtual ICollection<DailyEvent> DailyEvents { get; set; }
        public virtual ICollection<OccupationalAreasOfAssociation> OccupationalAreasOfAssociations { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
