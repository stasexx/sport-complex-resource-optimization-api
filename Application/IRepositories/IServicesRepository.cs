using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IServicesRepository : IBaseRepository<Service>
{
    Task<Service> UpdateServiceAsync(Service sportComplex, CancellationToken cancellationToken);

    Task<Service> RevealServiceAsync(Service service, CancellationToken cancellationToken);
}