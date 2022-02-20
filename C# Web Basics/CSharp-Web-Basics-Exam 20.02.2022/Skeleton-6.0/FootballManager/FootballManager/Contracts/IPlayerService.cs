using FootballManager.ViewModels;

namespace FootballManager.Contracts
{
    public interface IPlayerService
    {
        (bool created, string error, int playerId) Create(PlayerFormModel model);

        void AddPlayerToUser(int playerId, string userId);

        IEnumerable<PlayerListViewModel> GetAllPlayers();

        IEnumerable<PlayerListViewModel> GetMyPlayers(string userId);

        void RemovePlayer(int playerId,string userId);
    }
}
