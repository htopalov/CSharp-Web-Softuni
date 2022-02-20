using FootballManager.Contracts;
using FootballManager.Data.Common;
using FootballManager.Data.Models;
using FootballManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository repo;
        private readonly IValidationService validationService;

        public PlayerService(
            IRepository repo,
            IValidationService validationService)
        {
            this.repo = repo;
            this.validationService = validationService;
        }

        public (bool created, string error, int playerId) Create(PlayerFormModel model)
        {
            bool created = false;
            string error = null;

            var (isValid, validationError) = this.validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError, -1);
            }

            var player = new Player
            {
                FullName = model.FullName,
                Description = model.Description,
                Endurance = model.Endurance,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed
            };

            try
            {
                repo.Add(player);
                repo.SaveChanges();
                created = true;
            }
            catch (Exception)
            {
                error = "Could not save player";
            }

            return (created, error, player.Id);
        }

        public void AddPlayerToUser(int playerId, string userId)
        {
            var user = repo.All<User>()
                .Include(u=>u.UserPlayers)
                .FirstOrDefault(u => u.Id == userId);
            var player = repo.All<Player>()
                .FirstOrDefault(p => p.Id == playerId);

            if (user == null || player == null)
            {
                throw new ArgumentException("User or player not found");
            }

            var hasPlayer = user.UserPlayers.Any(u=> u.PlayerId == playerId);

            if (hasPlayer)
            {
                return;
            }

            user.UserPlayers.Add(new UserPlayer
            {
                PlayerId = playerId,
                Player = player,
                User = user,
                UserId = userId
            });

            repo.SaveChanges();
        }

        public IEnumerable<PlayerListViewModel> GetAllPlayers()
        {
            return repo.All<Player>()
                .Select(p => new PlayerListViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Endurance = p.Endurance,
                    FullName = p.FullName,
                    ImageUrl = p.ImageUrl,
                    Position = p.Position,
                    Speed = p.Speed
                });
        }

        public IEnumerable<PlayerListViewModel> GetMyPlayers(string userId)
        {
            var userPlayers = repo.All<UserPlayer>()
                .Include(us=>us.Player)
                .Where(up => up.UserId == userId)
                .ToList();

             return userPlayers
                .Select(p => new PlayerListViewModel
            {
                Id = p.Player.Id,
                Description = p.Player.Description,
                Endurance = p.Player.Endurance,
                FullName = p.Player.FullName,
                ImageUrl = p.Player.ImageUrl,
                Position = p.Player.Position,
                Speed = p.Player.Speed
                });
        }

        public void RemovePlayer(int playerId,string userId)
        {
            var player = repo.All<Player>().Where(p => p.Id == playerId).FirstOrDefault();
            var user = repo.All<User>().Where(u => u.Id == userId).FirstOrDefault();
            if (player != null && user != null)
            {
                repo.Remove<Player>(player);
                repo.SaveChanges();
            }
        }
    }
}
