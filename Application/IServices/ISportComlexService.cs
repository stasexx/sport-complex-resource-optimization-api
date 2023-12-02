using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface ISportComplexesService
{
    Task<SportComplexDto> CreateSportComplex(SportComplexCreateDto sportComplexCreateDto, string ownerId,
        CancellationToken cancellationToken);

    Task<SportComplexDto> UpdateSportComplex(SportComplexUpdateDto sportComplex,
        CancellationToken cancellationToken);

    Task<SportComplexDto> DeleteSportComplex(string sportComplexId, CancellationToken cancellationToken);

    Task<PagedList<SportComplexDto>> GetSportComplexesPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    
    Task<PagedList<SportComplexDto>> GetVisibleSportComplexesPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task<SportComplexDto> HideSportComplex(string sportComplexId, CancellationToken cancellationToken);

    Task<SportComplexDto> RevealSportComplex(string sportComplexId, CancellationToken cancellationToken);

    Task<PagedList<SportComplexDto>> GetVisibleSportComplexesByNamePages(int pageNumber, int pageSize,
        string partialName, CancellationToken cancellationToken);

    Task<SportComplexDto> GetSportComplexAsync(string id, CancellationToken cancellationToken);
}