using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface ISportComplexesService
{
    Task<SportComplexDto> CreateSportComplex(SportComplexCreateDto sportComplexCreateDto, string ownerId,
        CancellationToken cancellationToken);

    Task<SportComplexDto> UpdateSportComplex(SportComplexUpdateDto sportComplex,
        CancellationToken cancellationToken);
}