using library.worldcomputer.info;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;

namespace World.Controllers
{


    public partial class BodyController : ControllerBase
    {
        private IDal<Body> _bodyDal;

        public BodyController(IDal<Body> bodyDal)
        {
            _bodyDal = bodyDal;
        }


        [HttpGet("/body/{lastName}/{firstName}")]
        public async Task<IActionResult> GetBody(string firstName, string lastName)
        {
            var body = await _bodyDal.Get(IUnit.ToId($"{firstName} {lastName}" ));

            return new JsonResult(body);

        }

        [HttpGet("/body/counts")]
        public async Task<IActionResult> GetBoundCount()
        {
            var bodies = await _bodyDal.GetByAttr("Bound", true);

            return new JsonResult(bodies.Count());
        }


        public IActionResult ReserveBody(string familiyName, string firstName, string lastName)
        {

            throw new NotImplementedException();
        }




    }
}