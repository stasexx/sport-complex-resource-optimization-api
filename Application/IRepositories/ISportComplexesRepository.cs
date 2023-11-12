using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface ISportComplexesRepository : IBaseRepository<SportComplex>
{
    Task<SportComplex> UpdateSportComplexAsync(SportComplex sportComplex, CancellationToken cancellationToken);

    Task<SportComplex> RevealSportComplexAsync(SportComplex sportComplex, CancellationToken cancellationToken);
}
