using br.com.fiap.alert.api.Models;

namespace br.com.fiap.alert.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string username, string password);

    }
}