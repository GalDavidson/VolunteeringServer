using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VolunteeringServerBL.Models
{
    public partial class volunteeringDBContext : DbContext
    {
        public Association LoginAssociation(string email, string pass)
        {
            Association association = this.Users.Where(a => a.Email == email && a.Pass == pass).FirstOrDefault();
            return association;
        }

        public Volunteer LoginVolunteer(string email, string pass)
    }
}
