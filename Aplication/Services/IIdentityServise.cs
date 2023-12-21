using Domain.Entity;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public  interface IIdentityServise
    {
        Task<Response<GetUserModel>> RegisterAsync(User user);
        Task<Response< Token>> LoginAsync(Credential  credential);
        Task <Response<bool>> LogoutAsync();
        Task<Response<Token>> RefreshTokenAsync(Token token);
        Task<Response <bool > >DeleteUserAsync (int  UserId);
        Task <bool > SaveRefreshToken (string   refreshToken, User user);
        Task <bool  > IsValidRefreshToken (string   refreshToken, int userId);

        
    }
}
