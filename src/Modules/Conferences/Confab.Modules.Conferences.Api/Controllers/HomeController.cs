using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController:ApiBaseController
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Conferences API!";
        }
    }
}
