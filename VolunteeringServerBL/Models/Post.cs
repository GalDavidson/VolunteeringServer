using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public DateTime? ActionDate { get; set; }
        public string Caption { get; set; }
        public int? AssociationId { get; set; }
        public int? EventId { get; set; }

        public virtual Association Association { get; set; }
        public virtual DailyEvent Event { get; set; }
    }
}
