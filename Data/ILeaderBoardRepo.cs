using LeaderBoard.Models;

namespace LeaderBoard.Data
{
    public interface ILeaderBoardRepo
    {
        void UpsertPlayer(LeaderBoardPlayer player);
        void DeletePlayer(LeaderBoardPlayer player);
        LeaderBoardPlayer? GetPlayer(string id);
        LeaderBoardPlayer? GetPlayerByName(string name);
        IEnumerable<LeaderBoardPlayer?>? GetAllPlayers();
    }
}
