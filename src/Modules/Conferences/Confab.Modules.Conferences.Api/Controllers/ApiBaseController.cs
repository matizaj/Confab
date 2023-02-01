using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [ApiController]
    [Route(BasePath)]
    internal class ApiBaseController:ControllerBase
    {
        protected const string BasePath = "conferences-module";

        protected ActionResult<T> OkOrNotFound<T>(T model) 
        { 
            if(model is null) 
            {
                return NotFound();
            }

            return model;
        }
    }
}
