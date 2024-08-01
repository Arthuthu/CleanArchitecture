using CleanArchitectureFront.Entities;

namespace CleanArchitectureFront.APIService.UserService
{
    public interface IUserService
    {
        Task<List<User>?> GetAllUsers();
    }
}