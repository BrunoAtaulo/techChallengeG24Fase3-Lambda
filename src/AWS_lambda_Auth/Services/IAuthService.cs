using AWS_lambda_Auth.Models;

namespace AWS_lambda_Auth.Services
{
    public interface IAuthService
    {
        public Task<Token> Login(Login login);
        public Task Create(User user);
        public Task<Token> RefreshToken(string refreshToken);
        public dynamic GetChaves();
    }
}