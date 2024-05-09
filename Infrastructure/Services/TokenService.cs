using Aplication.Services;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateRefreshTokensAsync(User user)
        {

            return ComputeShA256Hash((DateTime.Now.ToString() + "MyKey"));
        }

        public string ComputeShA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<Token> GenerateTokensAsync(User user)
        {

            List<Claim> claims = new ()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim("Id", user.Id.ToString())

            };
            foreach (var role in user.Roles)
            {
                foreach (var permission in role.Permissions)
                {
                 claims.Add(new Claim("Permission", permission.Name));
                }
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            double accesTokenLifeTime = double.Parse(_configuration["JWT:AccessTokenLifeTime"]);

            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(accesTokenLifeTime),
              signingCredentials: credentials);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new()
            {
                AccessToken = accessToken,
                RefreshToken = await GenerateRefreshTokensAsync(user)
            };



        }

        public Task<Token> GetNewTokensAsync(Token token)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetCleamsFromExpireTokensAsync(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken);
            var claims = (jsonToken as JwtSecurityToken)?.Claims;
            var userClaims = claims?.ToArray();
            return new()
            {
                Id = int.Parse(userClaims.First(x => x.Type.Equals("Id")).Value),
                Name = userClaims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value

            };

        }

        public Task<Token> v(User user)
        {
            throw new NotImplementedException();
        }
    }
}
