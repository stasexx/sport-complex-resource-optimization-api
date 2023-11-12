using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IAbonnementsRepository : IBaseRepository<Abonnement>
{
    Task<int> GetDuration(string id);

    Task<Abonnement> RevealAbonnementAsync(string abonnementId, CancellationToken cancellationToken);

    Task<Abonnement> UpdateAbonnementAsync(Abonnement abonnement, CancellationToken cancellationToken);
}