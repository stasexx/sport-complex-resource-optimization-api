using System.Security.Claims;

namespace SportComplexResourceOptimizationApi.Application.IServices.Identity;

public interface ITokensService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

}