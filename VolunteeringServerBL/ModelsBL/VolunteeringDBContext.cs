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

        public VolunteersInEvent AddVolInEvent(VolunteersInEvent v)
        {
            try
            {
                this.Entry(v).State = EntityState.Added;
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

        public bool UpdateEv(DailyEvent updated)
        {
            try
            {
                this.ChangeTracker.Clear();
                ICollection<OccupationalAreasOfEvent> occu = this.OccupationalAreasOfEvents.Where(o => o.EventId == updated.EventId).ToList();
                //Loop through the DB records and check if any of them should be deleted
                foreach (OccupationalAreasOfEvent o in occu)
                {
                    OccupationalAreasOfEvent temp = updated.OccupationalAreasOfEvents.Where(a => a.OccupationalAreaId == o.OccupationalAreaId && a.EventId == o.EventId).FirstOrDefault();
                    if (temp == null)
                        this.Entry(o).State = EntityState.Deleted;
                    else
                        this.Entry(o).State = EntityState.Unchanged;

                }

                //loop through the updated user records and check if they need to be added to the DB
                foreach (OccupationalAreasOfEvent o in updated.OccupationalAreasOfEvents)
                {
                    OccupationalAreasOfEvent temp = occu.Where(a => a.OccupationalAreaId == o.OccupationalAreaId && a.EventId == o.EventId).FirstOrDefault();
                    if (temp == null)
                        this.Entry(o).State = EntityState.Added;
                }
                this.Entry(updated).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateVolRate(VolunteersInEvent v)
        {
            try
            {
                this.ChangeTracker.Clear();
               
                this.Entry(v).State = EntityState.Modified;

                int ratingSum = 0;
                int ratingTimes = 0;
                ICollection <VolunteersInEvent> lst = this.VolunteersInEvents.Where(vol => vol.VolunteerId == v.VolunteerId).ToList();
                foreach (VolunteersInEvent volInEv in lst)
                {
                    if (volInEv.RatingNum != 0)
                    {
                        ratingSum += volInEv.RatingNum;
                        ratingTimes += 1;
                    }
                }

                int average = ratingSum / ratingTimes;

                Volunteer vol = this.Volunteers.Where(volunteer => volunteer.VolunteerId == v.VolunteerId).FirstOrDefault();
                vol.AvgRating = average;

                this.Entry(vol).State = EntityState.Modified;

                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
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
                this.ChangeTracker.Clear();

                ICollection<OccupationalAreasOfAssociation> ocList = a.OccupationalAreasOfAssociations.ToList();
                foreach(OccupationalAreasOfAssociation occ in ocList)
                {
                    this.Entry(occ).State = EntityState.Deleted;
                }

                ICollection<BranchesOfAssociation> brList = a.BranchesOfAssociations.ToList();
                foreach (BranchesOfAssociation br in brList)
                {
                    this.Entry(br).State = EntityState.Deleted;
                }

                ICollection<DailyEvent> events = a.DailyEvents.ToList();
                foreach (DailyEvent de in events)
                {
                    List<VolunteersInEvent> volLst = de.VolunteersInEvents.ToList();
                    foreach (VolunteersInEvent v in volLst)
                    {
                        this.Entry(v).State = EntityState.Deleted;
                    }

                    List<OccupationalAreasOfEvent> OccuLst = de.OccupationalAreasOfEvents.ToList();
                    foreach (OccupationalAreasOfEvent o in OccuLst)
                    {
                        this.Entry(o).State = EntityState.Deleted;
                    }

                    this.Entry(de).State = EntityState.Deleted;
                }

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
                this.ChangeTracker.Clear();

                ICollection<VolunteersInEvent> volList = VolunteersInEvents.ToList();
                foreach (VolunteersInEvent vol in volList)
                {
                    if (vol.VolunteerId == v.VolunteerId)
                        this.Entry(vol).State = EntityState.Deleted;
                }

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

        public bool RemoveVolFromEv(DailyEvent de)
        {
            try
            {
                VolunteersInEvent v = de.VolunteersInEvents.FirstOrDefault();
                this.ChangeTracker.Clear();
                this.Entry(v).State = EntityState.Deleted;
                this.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public bool DeleteEv(DailyEvent de)
        {
            try
            {
                List<VolunteersInEvent> volLst = VolunteersInEvents.ToList();
                foreach (VolunteersInEvent v in volLst)
                {
                    this.Entry(v).State = EntityState.Deleted;
                }

                List<OccupationalAreasOfEvent> OccuLst = OccupationalAreasOfEvents.ToList();
                foreach (OccupationalAreasOfEvent v in OccuLst)
                {
                    this.Entry(v).State = EntityState.Deleted;
                }

                this.Entry(de).State = EntityState.Deleted;
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
