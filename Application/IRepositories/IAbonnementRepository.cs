using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IAbonnementsRepository : IBaseRepository<Abonnement>
{
    Task<int> GetDuration(string id);
}