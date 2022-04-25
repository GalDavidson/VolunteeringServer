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
            Object user = this.Associations.Where(a => a.Email == email && a.Pass == pass)
                .Include(u => u.OccupationalAreasOfAssociations)
                .Include(u => u.BranchesOfAssociations)
                .Include(u => u.DailyEvents).FirstOrDefault();
            if (user == null)
            {
                user = this.Volunteers.Where(v => v.Email == email && v.Pass == pass).Include(g => g.Gender).FirstOrDefault();
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
                this.ChangeTracker.Clear();

                this.Entry(updatedUser).State = EntityState.Modified;

                ICollection<OccupationalAreasOfAssociation> occu = this.OccupationalAreasOfAssociations.Where(o => o.AssociationId == updatedUser.AssociationId).ToList();
                //Loop through the DB records and check if any of them should be deleted
                foreach (OccupationalAreasOfAssociation o in occu)
                {
                    OccupationalAreasOfAssociation temp = updatedUser.OccupationalAreasOfAssociations.Where(a => a.OccupationalAreaId == o.OccupationalAreaId && a.AssociationId == o.AssociationId).FirstOrDefault();
                    if (temp == null)
                        this.Entry(o).State = EntityState.Deleted;
                    else
                        this.Entry(o).State = EntityState.Unchanged;

                }

                //loop through the updated user records and check if they need to be added to the DB
                foreach (OccupationalAreasOfAssociation o in updatedUser.OccupationalAreasOfAssociations)
                {
                    OccupationalAreasOfAssociation temp = occu.Where(a => a.OccupationalAreaId == o.OccupationalAreaId && a.AssociationId == o.AssociationId).FirstOrDefault();
                    if (temp == null)
                        this.Entry(o).State = EntityState.Added;
                }

                //Branches
                ICollection<BranchesOfAssociation> branches = this.BranchesOfAssociations.Where(o => o.AssociationId == updatedUser.AssociationId).ToList();
                //Loop through the DB records and check if any of them should be deleted
                foreach (BranchesOfAssociation b in branches)
                {
                    BranchesOfAssociation temp = updatedUser.BranchesOfAssociations.Where(a => a.BranchId == b.BranchId && a.AssociationId == b.AssociationId).FirstOrDefault();
                    if (temp == null)
                        this.Entry(b).State = EntityState.Deleted;
                    else
                        this.Entry(b).State = EntityState.Unchanged;

                }

                //loop through the updated user records and check if they need to be added to the DB
                foreach (BranchesOfAssociation b in updatedUser.BranchesOfAssociations)
                {
                    BranchesOfAssociation temp = branches.Where(a => a.BranchId == a.BranchId && a.AssociationId == b.AssociationId).FirstOrDefault();
                    if (temp == null)
                        this.Entry(b).State = EntityState.Added;
                }

                this.SaveChanges();

                
                return updatedUser;
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
                this.ChangeTracker.Clear();

                this.Entry(updatedUser).State = EntityState.Modified;
                user.GenderId = updatedUser.GenderId;
                
                this.SaveChanges();
                return updatedUser;
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

        public DailyEvent AddEvent(DailyEvent d, Association a)
        {
            try
            {
                this.DailyEvents.Update(d);

                ICollection<OccupationalAreasOfEvent> list = d.OccupationalAreasOfEvents.ToList();
                foreach (OccupationalAreasOfEvent o in list)
                {
                    OccupationalAreasOfAssociation temp = a.OccupationalAreasOfAssociations.Where(t => t.OccupationalAreaId == o.OccupationalAreaId).FirstOrDefault();
                    if (temp == null)
                    {
                        OccupationalAreasOfAssociation occ = new OccupationalAreasOfAssociation
                        {
                            AssociationId = a.AssociationId,
                            OccupationalAreaId = o.OccupationalAreaId
                        };
                        this.OccupationalAreasOfAssociations.Add(occ);
                    }
                }
                this.SaveChanges();
                return d;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
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
