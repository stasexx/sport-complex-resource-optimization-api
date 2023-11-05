using SportComplexResourceOptimizationApi.Application.Models;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IUserService
{
    Task AddUserAsync(UserDto dto, CancellationToken cancellationToken);
}
