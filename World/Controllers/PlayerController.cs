using library.worldcomputer.info;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using MongoDB.Bson;

namespace World.Controllers
{


    public partial class PlayerController : ControllerBase
    {

        [HttpGet("/player/{lastName}/{firstName}")]
        public IActionResult GetBody(string firstName, string lastName)
        {


            throw new NotImplementedException();

        }

        [HttpGet("/player/new/choices")]
        public IActionResult GetBodyChoices()
        {
            throw new NotImplementedException();

        }


        public IActionResult ReserveBody(string familiyName, string firstName, string lastName)
        {

            throw new NotImplementedException();
        }




    }
}