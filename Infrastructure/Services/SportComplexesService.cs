using Application.Models.Dtos;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class SportComplexesService : ISportComplexesService
{
    private readonly ISportComplexesRepository _sportComplexesRepository;

    private readonly IMapper _mapper;

    public SportComplexesService(ISportComplexesRepository sportComplexesRepository, IMapper mapper)
    {
        _sportComplexesRepository = sportComplexesRepository;
        _mapper = mapper;
    }

    public async Task<SportComplexDto> CreateSportComplex(SportComplexCreateDto sportComplexCreateDto, string ownerId,
        CancellationToken cancellationToken)
    {
        var complex = _mapper.Map<SportComplex>(sportComplexCreateDto);
        
        complex.CreatedById = ObjectId.Parse(ownerId);
        complex.CreatedDateUtc = DateTime.UtcNow;

        await _sportComplexesRepository.AddAsync(complex, cancellationToken);

        var result = new SportComplexDto()
        {
            Name = complex.Name,
            Address = complex.Address,
            Email = complex.Email,
            Rating = complex.Rating,
            Description = complex.Description
        };

        return result;
    }

    public async Task<SportComplexDto> UpdateSportComplex(SportComplexUpdateDto sportComplex,
        CancellationToken cancellationToken)
    {
        var complex = await _sportComplexesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sportComplex.Id), cancellationToken);

        this._mapper.Map(sportComplex, complex);
        
        var result = await _sportComplexesRepository.UpdateSportComplexAsync(complex, cancellationToken);
        
        return _mapper.Map<SportComplexDto>(result);
    }

}