
using LeaderBoard.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace LeaderBoard.Data
{
    public class LeaderBoardRepo : ILeaderBoardRepo
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly RedisKey _sortedSetKey;

        public LeaderBoardRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _sortedSetKey = "Leaderboard";
        }

        public void DeletePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaderBoardPlayer?>? GetAllPlayers()
        {
            var db = _redis.GetDatabase();

            var leaderboardPlayers = db.SortedSetRangeByRankWithScores(_sortedSetKey, start: 0, stop: -1, order: Order.Descending);

            foreach (var player in leaderboardPlayers)
            {
                var playerIdAndScore = player.ToString().Split(':');
                var playerId = playerIdAndScore[1];
                var playerName = playerIdAndScore[2];
                var score = playerIdAndScore[3];



                yield return new LeaderBoardPlayer
                {
                    Id = playerId,
                    Name = playerName,
                    Mmr = int.Parse(score)
                };
            }
        }

        public LeaderBoardPlayer? GetPlayer(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var db = _redis.GetDatabase();

            var player = db.HashGet("hashPlayer", id);

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

            db.SortedSetAdd(_sortedSetKey, $"{player.Id}:{player.Name}", player.Mmr);
        }

        public void UpdatePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
