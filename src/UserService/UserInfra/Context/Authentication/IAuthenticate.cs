namespace UserInfra.Context.Authentication
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string username, string password);
        Task<string> RegisterUser(string email, string password, string username);
        Task Logout();
    }
}
