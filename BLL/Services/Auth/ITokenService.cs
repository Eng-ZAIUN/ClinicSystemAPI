
using DAL.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Services.Auth
{
    public interface ITokenService
    {
        JwtSecurityToken CreateToken(ApplicationUser user, IList<string> roles, IList<Claim> userClaims);

    }
}
