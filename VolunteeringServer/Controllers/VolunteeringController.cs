using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VolunteeringServerBL.Models;

namespace VolunteeringServer.Controllers
{
    [Route("")]
    [ApiController]
    public class VolunteeringController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        volunteeringDBContext context;
        public VolunteeringController(volunteeringDBContext context)
        {
            this.context = context;
        }
        #endregion
    }
}
