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

        [HttpPost]
        public ActionResult<LeaderBoardPlayer> CreatePlayerOnLeaderBoard(LeaderBoardPlayer player)
        {
            _repo.InsertPlayer(player);

            return CreatedAtRoute(nameof(GetPlayerById), new { id = player.Id }, player);
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeaderBoardPlayer>> GetPlayers()
        {
            return Ok(_repo.GetAllPlayers());
        }
    }
}
