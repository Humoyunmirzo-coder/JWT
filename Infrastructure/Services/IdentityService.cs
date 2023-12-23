using Aplication.Services;
using Domain.Entity;
using Domain.Model;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityServise
    {
        private readonly ITokenService _tokenService;

        private readonly IdentityDbContext _dbContext;
        private readonly int _refreshTokenLifeTime;

        public IdentityService(ITokenService tokenService, IdentityDbContext dbContext, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _dbContext = dbContext;
            _refreshTokenLifeTime = int.Parse(configuration["JWT:RefreshtokenLifeTime"]);
        }


        public Task<Response<bool>> DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteUserAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidRefreshToken(string refreshToken, int userId)
        {
            RefreshToken? refreshTokenEntity;
            var res = _dbContext.RefreshTokens.Where(x => x.UserId.Equals(userId)
                                                               && x.RefreshTokenValue.Equals(refreshToken));
            if (res.Count() != 1)
                return false;

            refreshTokenEntity = res.First();
            if (refreshTokenEntity.ExpireTime < DateTime.Now)
                return false;

            return true;

        }

        public Task<Response<Token>> Login(Credential credential)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Token>> LoginAsync(Credential credential)
        {
            credential.Password = _tokenService.ComputeShA256Hash(credential.Password);
            User? user = _dbContext.Users.Include(x=>x.Roles).FirstOrDefault(x => x.UserName.Equals(credential.UserName)
                                                                                           && x.Password.Equals(credential.Password));
            if (user == null)
                return new("User not faund! ", 404);

            Token userToken = await _tokenService.GenerateTokensAsync(user);
            bool issuccess = await SaveRefreshToken(userToken.RefreshToken, user);
            return issuccess ? new(userToken) : new("Failed to Save refresh token");
        }



        public async Task<Response<bool>> LogoutAsync()
        {
            return new(true);
        }

        public async Task<Response<Token>> RefreshTokenAsync(Token token)
        {
            User user = await _tokenService.GetCleamsFromExpireTokensAsync(token.AccessToken);
            if (!await IsValidRefreshToken(token.RefreshToken, user.Id))
                return new("refresh token invalid");

            Token newToken = await _tokenService.GenerateTokensAsync(user);
            bool isSuccess = await SaveRefreshToken(newToken.RefreshToken, user);
            return isSuccess ? new(newToken) : new("Failed");
        }


        public async Task<Response<GetUserModel>> RegisterAsync(User user)
        {
            user.Password = _tokenService.ComputeShA256Hash(user.Password);
            await _dbContext.Users.AddAsync(user);
            int effectedRow = await _dbContext.SaveChangesAsync();
            if (effectedRow <= 0)
                return new("Operation Filed");
                Token token = await _tokenService.GenerateTokensAsync(user);
            bool IsSuccess = await SaveRefreshToken(token.RefreshToken, user);
            GetUserModel getUserModel = new()
            {
                token = token,
                user = user
            };
            return IsSuccess ? new((getUserModel)) : new("Failed");

        }

        public async Task<bool> SaveRefreshToken(string refreshToken, User user)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            RefreshToken? refreshTokenEntity;
            var res = _dbContext.RefreshTokens.Where(x => x.UserId.Equals(user.Id)
                                                             && x.RefreshTokenValue.Equals(refreshToken));
            if (res.Count() == 0)
            {
                refreshTokenEntity = new()
                {
                    ExpireTime = DateTime.UtcNow.AddMinutes(_refreshTokenLifeTime),
                    RefreshTokenValue = refreshToken,
                    UserId = user.Id
                };
                await _dbContext.RefreshTokens.AddAsync(refreshTokenEntity);


            }
            else if (res.Count() == 1)
            {
                refreshTokenEntity = res.First();
                refreshTokenEntity.RefreshTokenValue = refreshToken;
                refreshTokenEntity.ExpireTime = DateTime.Now.AddMinutes(_refreshTokenLifeTime);

                _dbContext.RefreshTokens.Update(refreshTokenEntity);
            }

            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;




        }


    }
}
