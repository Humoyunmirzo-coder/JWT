using Domain.Entity;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public  interface ITokenService
    {
        Task< Token> v (User user);
        Task< User> GetCleamsFromExpireTokensAsync (string  accessToken);
        Task< string > GenerateRefreshTokensAsync  (User user);
        Task< Token > GetNewTokensAsync  (Token token);
        string ComputeShA256Hash(string input);
        Task<Token> GenerateTokensAsync(User user);
    }
}
