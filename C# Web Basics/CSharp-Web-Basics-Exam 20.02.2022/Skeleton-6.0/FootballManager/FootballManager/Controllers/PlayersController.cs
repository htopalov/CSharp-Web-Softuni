using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Contracts;
using FootballManager.ViewModels;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayersController(
            Request request,
            IPlayerService playerService)
            : base(request)
            => this.playerService = playerService;

        [Authorize]
        public Response All()
        {
            IEnumerable<PlayerListViewModel> players = playerService.GetAllPlayers();
            return View(new {players, IsAuthenticated = true }, "/Players/All");
        }

        [Authorize]
        public Response Add() 
            => View(new { IsAuthenticated = true },"/Players/Add");

        [Authorize]
        [HttpPost]
        public Response Add(PlayerFormModel model)
        {
            var (created, error, playerId) = this.playerService.Create(model);
            if (!created)
            {
                return View(new { IsAuthenticated = true },"/Players/Add");
            }

            this.playerService.AddPlayerToUser(playerId,User.Id);

            return Redirect("/Players/All");
        }

        [Authorize]
        public Response Collection()
        {
            var collection = this.playerService
                .GetMyPlayers(User.Id);

            return View(new {IsAuthenticated = true, collection});
        }

        [Authorize]
        public Response AddToCollection(int playerId)
        {
            this.playerService.AddPlayerToUser(playerId,User.Id);

            return Redirect("/Players/All");
        }

        [Authorize]
        public Response RemoveFromCollection(int playerId)
        {
            this.playerService.RemovePlayer(playerId,User.Id);

            return Redirect(nameof(Collection));
        }
    }
}
