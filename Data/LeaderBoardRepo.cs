
using LeaderBoard.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace LeaderBoard.Data
{
    public class LeaderBoardRepo : ILeaderBoardRepo
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly RedisKey _sortedSetKey;

        public LeaderBoardRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
            _sortedSetKey = "Leaderboard";
        }

        public void DeletePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaderBoardPlayer?>? GetAllPlayers()
        {
            var leaderboardPlayers = _db.SortedSetRangeByRankWithScores(_sortedSetKey, start: 0, stop: -1, order: Order.Descending);

            foreach (var player in leaderboardPlayers)
            {
                var playerIdAndScore = player.ToString().Split(':');
                var playerId = playerIdAndScore[0];
                var playerName = playerIdAndScore[1];
                var score = playerIdAndScore[2];



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

            var player = _db.HashGet("hashPlayer", id);

            if (!string.IsNullOrEmpty(player))
            {
                return JsonSerializer.Deserialize<LeaderBoardPlayer>(player);
            }
            return null;
        }

        public void InsertPlayer(LeaderBoardPlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            _db.SortedSetAdd(_sortedSetKey, $"{player.Id}:{player.Name}", player.Mmr);
        }

        public void UpdatePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
