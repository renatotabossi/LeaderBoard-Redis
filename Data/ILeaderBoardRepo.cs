using LeaderBoard.Models;

namespace LeaderBoard.Data
{
    public interface ILeaderBoardRepo
    {
        void InsertPlayer(LeaderBoardPlayer player);
        void UpdatePlayer(LeaderBoardPlayer player);
        void DeletePlayer(LeaderBoardPlayer player);
        LeaderBoardPlayer? GetPlayer(string id);
        IEnumerable<LeaderBoardPlayer?>? GetAllPlayers();
    }
}
