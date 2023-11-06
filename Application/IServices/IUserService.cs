using SportComplexResourceOptimizationApi.Application.Models;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IUserService
{
    Task AddUserAsync(UserCreateDto dto, CancellationToken cancellationToken);
}
