using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Branch
    {
        public Branch()
        {
            BranchesOfAssociations = new HashSet<BranchesOfAssociation>();
        }

        public int BranchId { get; set; }
        public string BranchLocation { get; set; }

        public virtual ICollection<BranchesOfAssociation> BranchesOfAssociations { get; set; }
    }
}
