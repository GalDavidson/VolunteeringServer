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

        public Association RegisterAsso(Association a)
        {
            try
            {
                this.Associations.Add(a);
                this.SaveChanges();
                return a;
            }

            catch (Exception e)
            {
                return null;
            }

        }


        public bool AddOccupationalArea(OccupationalArea area)
        {
            try
            {
                this.OccupationalAreas.Add(area);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }


}
