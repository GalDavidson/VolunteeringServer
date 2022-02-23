﻿using System;
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

        //public Association UpdateAsso(Association a)
        //{
        //    try
        //    {
        //        this.Associations.Where(Association.AssociationID == a.AssociationId).Update;
        //        this.SaveChanges();
        //        return a;
        //    }

        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return null;
        //    }

        //}

        public Volunteer UpdateVol(Volunteer v)
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

    }
}
