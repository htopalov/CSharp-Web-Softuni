using FootballManager.ViewModels;

namespace FootballManager.Contracts
{
    public interface IUserService
    {
        (bool registered, string error) Register(RegisterViewModel model);

        string Login(LoginViewModel model);
    }
}
