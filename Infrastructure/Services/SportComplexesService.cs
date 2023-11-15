using Application.Models.Dtos;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;
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
            City = complex.City,
            Rating = complex.Rating,
            Description = complex.Description
        };

        return result;
    }

    public async Task<SportComplexDto> UpdateSportComplex(SportComplexUpdateDto sportComplex,
        CancellationToken cancellationToken)
    {
        var complex = await _sportComplexesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sportComplex.Id), cancellationToken);

        var updatedSportComplex = this._mapper.Map(sportComplex, complex);
        updatedSportComplex.LastModifiedDateUtc = DateTime.UtcNow;
        updatedSportComplex.LastModifiedById = complex.CreatedById;
        var result = await _sportComplexesRepository.UpdateSportComplexAsync(complex, cancellationToken);
        
        return _mapper.Map<SportComplexDto>(result);
    }

    public async Task<SportComplexDto> DeleteSportComplex(string sportComplexId, CancellationToken cancellationToken)
    {
        var complex = await _sportComplexesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sportComplexId), cancellationToken);

        if (complex == null)
        {
            throw new Exception("Complex was not found!");
        }

        await _sportComplexesRepository.DeleteFromDbAsync(complex, cancellationToken);

        return _mapper.Map<SportComplexDto>(complex);
    }

    public async Task<SportComplexDto> HideSportComplex(string sportComplexId, CancellationToken cancellationToken)
    {
        var complex = await _sportComplexesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sportComplexId), cancellationToken);

        if (complex == null)
        {
            throw new Exception("Complex was not found!");
        }

        await _sportComplexesRepository.DeleteAsync(complex, cancellationToken);

        return _mapper.Map<SportComplexDto>(complex);
    }
    
    public async Task<SportComplexDto> GetSportComplexAsync(string id, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            throw new InvalidDataException("Provided id is invalid.");
        }

        var entity = await _sportComplexesRepository.GetOneAsync(objectId, cancellationToken);
        if (entity == null)
        {
            throw new Exception(id);
        }

        return _mapper.Map<SportComplexDto>(entity);
    }
    
    public async Task<SportComplexDto> RevealSportComplex(string sportComplexId, CancellationToken cancellationToken)
    {
        var sportComplex = await _sportComplexesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sportComplexId), cancellationToken);

        if (sportComplex == null)
        {
            throw new Exception("Service was not found!");
        }

        await _sportComplexesRepository.RevealSportComplexAsync(sportComplex, cancellationToken);

        return _mapper.Map<SportComplexDto>(sportComplex);
    }
    
    public async Task<PagedList<SportComplexDto>> GetSportComplexesPages(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _sportComplexesRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
        var dtos = _mapper.Map<List<SportComplexDto>>(entities);
        var count = await _sportComplexesRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<SportComplexDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<PagedList<SportComplexDto>> GetVisibleSportComplexesPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _sportComplexesRepository.GetPageAsync(pageNumber, pageSize, x=> x.IsDeleted==false, cancellationToken);
        var dtos = _mapper.Map<List<SportComplexDto>>(entities);
        var count = await _sportComplexesRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<SportComplexDto>(dtos, pageNumber, pageSize, count);
    }
    
    public async Task<PagedList<SportComplexDto>> GetVisibleSportComplexesByNamePages(int pageNumber, int pageSize, 
        string partialName, CancellationToken cancellationToken)
    {
        var partialNameLower = partialName.ToLowerInvariant();
        var entities = await _sportComplexesRepository.GetPageAsync(pageNumber, pageSize,
            x => !x.IsDeleted && x.Name.ToLowerInvariant().Contains(partialNameLower),
            cancellationToken);
    
        var sortedEntities = entities
            .OrderBy(x => CompareSimilarity(x.Name, partialNameLower))
            .ToList();

        var dtos = _mapper.Map<List<SportComplexDto>>(sortedEntities);
        var count = await _sportComplexesRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<SportComplexDto>(dtos, pageNumber, pageSize, count);
    }
    
    public int CompareSimilarity(string str1, string str2)
    {
        return Math.Abs(str1.Length - str2.Length);
    }

}