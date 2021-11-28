using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VolunteeringServerBL.Models
{
    public partial class VolunteeringDBContext : DbContext
    {
        public Object Login(string email, string pass)
        {
            Object user = this.Associations.Where(a => a.Email == email && a.Pass == pass).FirstOrDefault();
            if (user == null)
            {
                user = this.Volunteers.Where(v => v.Email == email && v.Pass == pass).FirstOrDefault();
            }
            return user;
        }
    }

    public partial class VolunteeringDBContext : DbContext
    {
        public Association RegisterAsso(string email, string userName, string infAbout, string phoneNum, string pass, string profilePic)
        {
            Association a = new Association(email, userName, infAbout, phoneNum, pass, profilePic);
            if (user == null)
            {
                user = this.Volunteers.Where(v => v.Email == email && v.Pass == pass).FirstOrDefault();
            }
            return user;
        }
    }
}
