using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolunteeringServerBL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
    }   
}
