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
            if (user == null)
            {
                user = this.AppAdmins.Where(v => v.Email == email && v.Pass == pass).FirstOrDefault();
            }
            return user;
        }

        public Association RegisterAsso(Association a)
        {
            try
            {
                this.Associations.Update(a);
                this.SaveChanges();
                return a;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public Volunteer RegisterVol(Volunteer v)
        {
            try
            {
                this.Volunteers.Update(v);
                this.SaveChanges();
                return v;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public Association UpdateAsso(Association user, Association updatedUser)
        {
            try
            {
                Association currentUser = this.Associations
                .Where(a => a.AssociationId == user.AssociationId).FirstOrDefault();

                currentUser.Email = updatedUser.Email;
                currentUser.UserName = updatedUser.UserName;
                currentUser.InformationAbout = updatedUser.InformationAbout;
                currentUser.Pass = updatedUser.Pass;
                currentUser.PhoneNum = updatedUser.PhoneNum;

                DbSet<OccupationalAreasOfAssociation> occu = this.OccupationalAreasOfAssociations;
                foreach(OccupationalAreasOfAssociation o in occu)
                {
                    if (o.AssociationId == user.AssociationId)
                    {
                        this.OccupationalAreasOfAssociations.Remove(o);
                    }
                }

                ICollection<OccupationalAreasOfAssociation> occuAreas = updatedUser.OccupationalAreasOfAssociations;
                foreach (OccupationalAreasOfAssociation o in occuAreas)
                {
                    this.OccupationalAreasOfAssociations.Update(o);
                }

                DbSet<BranchesOfAssociation> brn = this.BranchesOfAssociations;
                foreach (BranchesOfAssociation b in brn)
                {
                    if (b.AssociationId == user.AssociationId)
                    {
                        this.BranchesOfAssociations.Remove(b);
                    }
                }

                ICollection<BranchesOfAssociation> brAsso = updatedUser.BranchesOfAssociations;
                foreach (BranchesOfAssociation b in brAsso)
                {
                    this.BranchesOfAssociations.Update(b);
                }

                this.SaveChanges();
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public Volunteer UpdateVol(Volunteer user, Volunteer updatedUser)
        {
            try
            {
                Volunteer currentUser = this.Volunteers
                .Where(a => a.VolunteerId == user.VolunteerId).FirstOrDefault();

                currentUser.Email = updatedUser.Email;
                currentUser.UserName = updatedUser.UserName;
                currentUser.FName = updatedUser.LName;
                currentUser.LName = updatedUser.LName;
                currentUser.Pass = updatedUser.Pass;
                currentUser.GenderId = updatedUser.GenderId;
                currentUser.BirthDate = updatedUser.BirthDate;

                this.SaveChanges();
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        public bool AddBranch(Branch b)
        {
            try
            {
                this.Branches.Add(b);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddGender(Gender g)
        {
            try
            {
                this.Genders.Add(g);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public bool RemoveAsso(Association a)
        {
            try
            {
                this.Associations.Remove(a);
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool RemoveVol(Volunteer v)
        {
            try
            {
                this.Volunteers.Remove(v);
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
