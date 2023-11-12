using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
}