using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolunteeringServerBL.Models;

namespace VolunteeringServer.DTO
{
    public class Lookups
    {
        public List<OccupationalArea> OccupationalAreas { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Area> Areas { get; set; }
        public List<Gender> Genders { get; set; }
        public List<Rank> Ranks { get; set; }
    }
}
