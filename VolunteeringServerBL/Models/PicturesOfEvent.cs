using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class PicturesOfEvent
    {
        public int PicId { get; set; }
        public string PicUrl { get; set; }
        public int? EventId { get; set; }

        public virtual DailyEvent Event { get; set; }
    }
}
