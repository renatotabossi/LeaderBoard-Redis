
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

        public IEnumerable<LeaderBoardPlayer?>? GetAllPlayers()
        {
            var db = _redis.GetDatabase();

            var completeSet = db.SetMembers("PlayersSet");

            if ( completeSet.Length > 0) 
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<LeaderBoardPlayer>(val)).ToList();
                return obj;
            }

            return null;
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

            // Usar isso para fazer a leaderboard em si ! ! !
            db.SetAdd("PlayersSet", serialPlayer);
        }

        public void UpdatePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
