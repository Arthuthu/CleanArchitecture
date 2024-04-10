namespace UserInfra.Context.Authentication
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<string> RegisterUser(string email, string password);
        Task Logout();
    }
}
