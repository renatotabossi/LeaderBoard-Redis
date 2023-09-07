
using LeaderBoard.Models;
using StackExchange.Redis;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;

namespace LeaderBoard.Data
{
    public class LeaderBoardRepo : ILeaderBoardRepo
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private readonly RedisKey _sortedSetLBKey;
        private readonly RedisKey _hashedSetPlayerKey;
        private readonly RedisKey _hashedSetPlayerByNameKey;

        public LeaderBoardRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
            _sortedSetLBKey = "Leaderboard";
            _hashedSetPlayerKey = "Players";
            _hashedSetPlayerByNameKey = "PlayersByName";
        }

        public void DeletePlayer(LeaderBoardPlayer player)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LeaderBoardPlayer?>? GetAllPlayers()
        {
            var leaderboardPlayers = _db.SortedSetRangeByRankWithScores(_sortedSetLBKey, start: 0, stop: -1, order: Order.Descending);

            foreach (var player in leaderboardPlayers)
            {
                var playerIdAndScore = player.ToString().Split(':');
                var playerId = playerIdAndScore[0];
                var score = playerIdAndScore[1];

                var playerName = _db.HashGet(_hashedSetPlayerKey, playerId);
                var playerRank = _db.SortedSetRank(_sortedSetLBKey, playerId, Order.Descending);


                yield return new LeaderBoardPlayer
                {
                    Rank = (int)playerRank! + 1,
                    Id = playerId,
                    Name = playerName,
                    Mmr = int.Parse(score)
                };
            }
        }

        public LeaderBoardPlayer? GetPlayer(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
           
            var playerName = _db.HashGet(_hashedSetPlayerKey, id);
            var playerRank = _db.SortedSetRank(_sortedSetLBKey, id);
            var playerScore = _db.SortedSetScore(_sortedSetLBKey, id);

            if (playerScore == 0)
            {
                return null;
            }

            return new LeaderBoardPlayer
            {
                Rank = (int)playerRank!,
                Id = id,
                Name = playerName,
                Mmr = (int)playerScore!
            };
        }

        public LeaderBoardPlayer? GetPlayerByName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            
            var playerId = _db.HashGet(_hashedSetPlayerByNameKey, name);
            var playerRank = _db.SortedSetRank(_sortedSetLBKey, playerId);
            var playerScore = _db.SortedSetScore(_sortedSetLBKey, playerId);
            var playerName = name;


            if (playerScore == 0)
            {
                return null;
            }

            return new LeaderBoardPlayer
            {
                Rank = (int)playerRank!,
                Id = playerId,
                Name = playerName,
                Mmr = (int)playerScore!
            };
        }

        public void UpsertPlayer(LeaderBoardPlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (player.Mmr != 0)
            {
                _db.SortedSetAdd(_sortedSetLBKey, player.Id, player.Mmr);
            } else
            {
                _db.SortedSetIncrement(_sortedSetLBKey, player.Id, 20);
            }

            _db.HashSet(_hashedSetPlayerKey, player.Id, player.Name);
            _db.HashSet(_hashedSetPlayerByNameKey, player.Name, player.Id);
        }
    }
}
