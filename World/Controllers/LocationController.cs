using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using library.worldcomputer.info;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Serializers;



namespace World.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class LocationController : ControllerBase
    {
        private IGrid _grid;

        public LocationController(IGrid grid)
        {
            _grid = grid;    
        }

        [HttpGet("/location/{layer}/{x}/{y}/")]
        public IActionResult GetLocation(int layer, int x, int y) {
            
            var locr = new LocationResult();
            var pair = _grid[layer][x][y];
            var pg = new PairGroup(_grid, pair);
            
            //mire dwellers can only see thier "current" location, and "up"
            if (layer == 0) {
                pg.South = Pair.None;
                pg.East = Pair.None;
                pg.North = Pair.None;
                pg.West = Pair.None;
                pg.Down = Pair.None;
                locr.PairGroups.Add(pg);
            }
            //ascended and aepherum walk on level 1 
            else if (layer == 1)
            {
                locr.PairGroups.Add(pg);
                locr.PairGroups.Add(new PairGroup(_grid, pg.Down));
            }
            else if (layer == 2)
            {
                locr.PairGroups.Add(pg);
                var down = new PairGroup(_grid, pg.Down);
                locr.PairGroups.Add(down);
                locr.PairGroups.Add(new PairGroup(_grid, down.Down));
                locr.PairGroups.Add(new PairGroup(_grid, down.North));
                locr.PairGroups.Add(new PairGroup(_grid, down.South));
                locr.PairGroups.Add(new PairGroup(_grid, down.East));
                locr.PairGroups.Add(new PairGroup(_grid, down.West));
            }
            else
            {
                return StatusCode(400, "Bad Reqeust. Layer not supported.");
            }

            return new JsonResult(locr);
        }
    }

    public class LocationResult 
    {
        public List<PairGroup> PairGroups {get;set;}
        public Direction[] EgressDirections {get;set;}

        public LocationResult()
        {
            PairGroups = new List<PairGroup>();
        }
    }
}