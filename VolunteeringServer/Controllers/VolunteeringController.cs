﻿using Microsoft.AspNetCore.Http;
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
                    Genders = context.Genders.ToList(),
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
                AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theAdmin");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null && user.AdminName != "")
                {
                    return context.Associations.ToList();
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
                AppAdmin user = HttpContext.Session.GetObject<AppAdmin>("theAdmin");
                //Check if user logged in and its ID is the same as the contact user ID
                if (user != null && user.AdminName != "")
                {
                    return context.Volunteers.ToList();
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


        [Route("RegisterAsso")]
        [HttpPost]
         
        public Association RegisterAsso ([FromBody] Association a)
        {
            if (a != null)
            {
                this.context.RegisterAsso(a);

                HttpContext.Session.SetObject("theAssociation", a);
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

                HttpContext.Session.SetObject("theVolunteer", v);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return v;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }

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
    }   
}
