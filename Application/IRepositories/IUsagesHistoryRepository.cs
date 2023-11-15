using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IUsagesHistoryRepository : IBaseRepository<UsagesHistory>
{
    Task UpdateUsages(string id);
}