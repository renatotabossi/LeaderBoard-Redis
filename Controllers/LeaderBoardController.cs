using LeaderBoard.Data;
using LeaderBoard.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoard.Controllers
{
    [Route("api/LeaderBoard")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly ILeaderBoardRepo _repo;

        public LeaderBoardController(ILeaderBoardRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name= "GetPlayerById")]
        public ActionResult<LeaderBoardPlayer> GetPlayerById(string id)
        {
            var player = _repo.GetPlayer(id);

            if (player != null)
            {
                return Ok(player);
            }

            return NotFound();
        }

        [HttpGet("name/{name}", Name = "GetPlayerByName")]
        public ActionResult<LeaderBoardPlayer> GetPlayerByName(string name)
        {
            var player = _repo.GetPlayerByName(name);

            if (player != null)
            {
                return Ok(player);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<LeaderBoardPlayer> CreatePlayerOnLeaderBoard(LeaderBoardPlayer player)
        {
            _repo.UpsertPlayer(player);

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeaderBoardPlayer>> GetPlayers()
        {
            return Ok(_repo.GetAllPlayers());
        }
    }
}
