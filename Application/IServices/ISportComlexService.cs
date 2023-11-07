using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface ISportComplexesService
{
    Task<SportComplexDto> CreateSportComplex(SportComplexCreateDto sportComplexCreateDto, string ownerId,
        CancellationToken cancellationToken);
}