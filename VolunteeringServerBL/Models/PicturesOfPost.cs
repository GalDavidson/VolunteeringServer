using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class PicturesOfPost
    {
        public int PicId { get; set; }
        public string PicUrl { get; set; }
        public int? PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
