using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolunteeringServerBL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using VolunteeringServer.DTO;

namespace VolunteeringServer.Controllers
{
    [Route("VolunteeringAPI")]
    [ApiController]
    public class VolunteeringController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        VolunteeringDBContext context;
        public VolunteeringController(VolunteeringDBContext context)
        {
            this.context = context;
        }
        #endregion

        [Route("Login")]
        [HttpGet]
        public Object Login([FromQuery] string email, [FromQuery] string pass)
        {
            Object user = context.Login(email, pass);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return user;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("UpdateAssociation")]
        [HttpPost]
        public Association UpdateAsso([FromBody] Association user)
        {
            //If user is null the request is bad
            if (user == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Association currentUser = HttpContext.Session.GetObject<Association>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.AssociationId == user.AssociationId)
            {
                Association updatedUser = context.UpdateAsso(currentUser, user);

                if (updatedUser == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedUser;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("UpdateVolunteer")]
        [HttpPost]
        public Volunteer UpdateVol([FromBody] Volunteer user)
        {
            //If user is null the request is bad
            if (user == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            Volunteer currentUser = HttpContext.Session.GetObject<Volunteer>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && currentUser.VolunteerId == user.VolunteerId)
            {
                Volunteer updatedUser = context.UpdateVol(currentUser, user);

                if (updatedUser == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return updatedUser;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }


        [Route("UpdateEvent")]
        [HttpPost]
        public bool UpdateEv([FromBody] DailyEvent e)
        {
            //If user is null the request is bad
            if (e == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }

            Association currentUser = HttpContext.Session.GetObject<Association>("theUser");
            
            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null && e != null) 
            {
                bool success = context.UpdateEv(e);

                if (!success)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("UpdateRating")]
        [HttpPost]
        public bool UpdateR([FromBody] VolunteersInEvent vol)
        {
            //If user is null the request is bad
            if (vol == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }

            Association currentUser = HttpContext.Session.GetObject<Association>("theUser");

            //Check if user logged in and its ID is the same as the contact user ID
            if (currentUser != null)
            {
                bool success = context.UpdateVolRate(vol);

                if (!success)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }


        [Route("GetLookups")]
        [HttpGet]
        public Lookups GetLookups()
        {
            try
            {
                Lookups obj = new Lookups()
                {
                    OccupationalAreas = context.OccupationalAreas.ToList(),
                    Branches = context.Branches.ToList(),
                    Areas = context.Areas.ToList(),
                    Genders = context.Genders.ToList(),
                    Ranks = context.Ranks.ToList()
                };

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return obj;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("GetAllAssociations")]
        [HttpGet]
        public List<Association> GetAllAssociations()
        {
            try
            {
                AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null && user.AdminName != "")
                {
                    return context.Associations
                        .Include(a => a.OccupationalAreasOfAssociations).ThenInclude(oc => oc.OccupationalArea)
                        .Include(a => a.BranchesOfAssociations)
                        .ThenInclude(b => b.Branch)
                        .Include(a => a.DailyEvents)
                        .ThenInclude(v => v.VolunteersInEvents)
                        .Include(a => a.DailyEvents)
                        .ThenInclude(o => o.OccupationalAreasOfEvents)
                        .ThenInclude(occ => occ.OccupationalArea)
                        .ToList();
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }   
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("GetAllVolunteers")]
        [HttpGet]
        public List<Volunteer> GetAllVolunteers()
        {
            try
            {
                AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null && user.AdminName != "")
                {
                    return context.Volunteers.Include(g => g.Gender).Include(v => v.VolunteersInEvents).ToList();
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("GetAllVolsInEvents")]
        [HttpGet]
        public List<VolunteersInEvent> GetAllVolsInEvents()
        {
            try
            {
                AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theUser");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null && user.AdminName != "")
                {
                    return context.VolunteersInEvents.ToList();
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("GetAllEvents")]
        [HttpGet]
        public List<DailyEvent> GetAllEvents()
        {
            try
            {
                return context.DailyEvents.Include(o => o.OccupationalAreasOfEvents).Include(u => u.VolunteersInEvents).ThenInclude(v => v.Volunteer).ThenInclude(h => h.Gender).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [Route("RegisterAsso")]
        [HttpPost]
         
        public Association RegisterAsso ([FromBody] Association a)
        {
            if (a != null)
            {
                this.context.RegisterAsso(a);

                HttpContext.Session.SetObject("theUser", a);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return a;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }

        }

        [Route("RegisterVolunteer")]
        [HttpPost]

        public Volunteer RegisterVolunteer([FromBody] Volunteer v)
        {
            if (v != null)
            {
                this.context.RegisterVol(v);

                HttpContext.Session.SetObject("theUser", v);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return v;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }

        }

        [Route("Logout")]
        [HttpPost]
        public bool Logout([FromBody] Object user)
        {
            try
            {
                Object current = HttpContext.Session.GetObject<Object>("theUser");

                if (current != null)
                {
                    HttpContext.Session.Remove("theUser");
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return true;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }


        [Route("AddNewVolInEvent")]
        [HttpPost]

        public VolunteersInEvent AddVolInEvent([FromBody] VolunteersInEvent v)
        {
            Volunteer vol = HttpContext.Session.GetObject<Volunteer>("theUser");
            //Check if user logged in and its name isn't null
            if (vol != null && vol.FName != "")
            {
                if (v != null)
                {
                    VolunteersInEvent volInEvent = context.AddVolInEvent(v);
                    if (volInEvent != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return volInEvent;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                        return null;
                    }
                }
                else
                { 
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }   
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }


        [Route("CreateNewEvent")]
        [HttpPost]
        public DailyEvent NewEvent([FromBody] DailyEvent e)
        {
            Association a = HttpContext.Session.GetObject<Association>("theUser");
            //Check if user logged in
            if (a != null)
            {
                if (e != null)
                {
                    DailyEvent added = this.context.AddEvent(e, a);
                    if (added != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return added;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                        return null;
                    }
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }
            return null;
        }


        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            Object user = HttpContext.Session.GetObject<Object>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("AddOccupationalArea")]
        [HttpPost]
        public bool AddOccuArea([FromBody] OccupationalArea occupationalArea)
        {

            if (occupationalArea != null)
            {
                bool added = this.context.AddOccupationalArea(occupationalArea);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return added;
                }
                else
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("AddBranch")]
        [HttpPost]
        public bool AddB([FromBody] Branch branch)
        {
            if (branch != null)
            {
                bool added = this.context.AddBranch(branch);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return added;
                }
                else
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("AddGender")]
        [HttpPost]
        public bool AddG([FromBody] Gender gender)
        {
            if (gender != null)
            {
                bool added = this.context.AddGender(gender);
                if (added)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return added;
                }
                else
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route("RemoveAsso")]
        [HttpPost]
        public bool RemoveAssociation([FromBody] Association a)
        {
            AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theUser");
            //Check if user logged in and its name isn't null
            if (user != null && user.AdminName != "")
            {
                if (a != null)
                {
                    bool success = this.context.RemoveAsso(a);
                    if (success)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return success;
                    }
                    else
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            return false;
        }

        [Route("RemoveVol")]
        [HttpPost]
        public bool RemoveVolunteer([FromBody] Volunteer v)
        {
            AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theUser");
            //Check if user logged in and its name isn't null
            if (user != null && user.AdminName != "")
            {
                if (v != null)
                {
                    bool success = this.context.RemoveVol(v);
                    if (success)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return success;
                    }
                    else
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            return false;
        }


        [Route("DeleteEvent")]
        [HttpPost]
        public bool Delete([FromBody] DailyEvent e)
        {
            Association user = HttpContext.Session.GetObject<Association>("theUser");
            //Check if user logged in and its name isn't null
            if (user != null && user.InformationAbout != "")
            {
                if (e != null && user.AssociationId == e.AssociationId)
                {
                    bool success = this.context.DeleteEv(e);
                    if (success)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return success;
                    }
                    else
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            return false;
        }


        [Route("RemoveVolFromEvent")]
        [HttpPost]
        public bool RemoveVol([FromBody] DailyEvent e)
        {
            Object user = null;
            Association userAccosiation = HttpContext.Session.GetObject<Association>("theUser");
            if (userAccosiation.AssociationId == 0)
            {
                user = HttpContext.Session.GetObject<Volunteer>("theUser");
            }
            else
            {
                user = userAccosiation;
            }
            
            //Check if user logged in and its name isn't null
            if (user != null && (user is Association || user is Volunteer))
            {
                if (e != null)
                {
                    bool success = this.context.RemoveVolFromEv(e);
                    if (success)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return success;
                    }
                    else
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }
            return false;
        }
    }   
}
