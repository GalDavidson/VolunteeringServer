using System;
using System.Collections.Generic;

#nullable disable

namespace VolunteeringServerBL.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Volunteers = new HashSet<Volunteer>();
        }

        public int GenderId { get; set; }
        public string GenderType { get; set; }

        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}
