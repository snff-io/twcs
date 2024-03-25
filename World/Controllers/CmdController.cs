using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using library.worldcomputer.info;
using System.Text.Json.Serialization;



namespace World.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class CmdController : ControllerBase
    {

        IMove _mover;

        public CmdController(IMove mover)
        {
            _mover = mover;
        }

        [HttpGet("/cmd/{cmd}")]
        public bool ExecCmd(string cmd){
            var cmde = cmd.Split('.');
            //move.cl.cx.cy.direction
            switch (cmde[0])
            {
                case "move":

                    var l = int.Parse(cmde[1]);
                    var x = int.Parse(cmde[2]);
                    var y = int.Parse(cmde[3]);
                    var d = Enum.Parse<Direction>(cmde[4]);

                    return _mover.TryMove(new Location{Layer=l, X=x, Y=y }, d);
                default:
                    return false;
            }
        }
    }
}