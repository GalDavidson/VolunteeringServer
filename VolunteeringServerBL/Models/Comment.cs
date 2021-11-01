using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int EventId { get; set; }
        public int VolunteerId { get; set; }

        public virtual VolunteersInEvent VolunteersInEvent { get; set; }
    }
}
