using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IServiceSubscriptionsRepository : IBaseRepository<ServiceSubscription>
{
    Task UpdateUsages(string id);
}