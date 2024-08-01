using CleanArchitectureFront.Entities;
using System.Text.Json;

namespace CleanArchitectureFront.APIService.UserService
{
    public class UserService : IUserService
    {
        private string _websiteUrl = $"https://localhost:7115/v1/user";

        public async Task<List<User>?> GetAllUsers()
        {
            using (HttpClient httpClient = new())
            {
                string getAllUsersEndpoint = $"{_websiteUrl}/get";

                HttpResponseMessage response = await httpClient.GetAsync(getAllUsersEndpoint);

                if(response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();


                    JsonSerializerOptions options = new()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    List<User>? users = JsonSerializer.Deserialize<List<User>>(responseBody, options);

                    return users;
                }

                return null;
            }
        }
    }
}
