
using LeaderBoard.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace LeaderBoard.Data
{
    public class LeaderBoardRepo : ILeaderBoardRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public LeaderBoardRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void DeletePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaderBoardPlayer> GetAllPlayers()
        {
            throw new NotImplementedException();
        }

        public LeaderBoardPlayer? GetPlayer(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var db = _redis.GetDatabase();
            var player = db.StringGet(id);

            if (!string.IsNullOrEmpty(player))
            {
                return JsonSerializer.Deserialize<LeaderBoardPlayer>(player);
            }
            return null;
        }

        public void InsertPlayer(LeaderBoardPlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            var db = _redis.GetDatabase();

            var serialPlayer = JsonSerializer.Serialize(player);

            db.StringSet(player.Id, serialPlayer);
        }

        public void UpdatePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
