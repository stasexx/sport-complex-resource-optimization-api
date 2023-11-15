using Application.Models.Dtos;
using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class EquipmentsService : IEquipmentService
{
    private readonly IEquipmentsRepository _equipmentRepository;

    private readonly ISensorsRepository _sensorsRepository;

    private readonly IUsagesHistoryRepository _usagesHistoryRepository;

    private readonly IMapper _mapper;

    public EquipmentsService(IEquipmentsRepository equipmentRepository, ISensorsRepository sensorsRepository,
        IUsagesHistoryRepository usagesHistoryRepository, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _sensorsRepository = sensorsRepository;
        _usagesHistoryRepository = usagesHistoryRepository;
        _mapper = mapper;
    }

    public async Task<EquipmentDto> CreateEquipment(EquipmentDto dto, string serviceId, string userId,
        CancellationToken cancellationToken)
    {
        var equipment = new Equipment()
        {
            Name = dto.Name,
            ServiceId = ObjectId.Parse(serviceId),
            CreatedById = ObjectId.Parse(userId),
            CreatedDateUtc = DateTime.UtcNow
        };
        
        var result = await _equipmentRepository.AddAsync(equipment, cancellationToken);

        var history = new UsagesHistory()
        {
            EquipmentId = result.Id,
            TotalUsages = 0
        };

        await _usagesHistoryRepository.AddAsync(history, cancellationToken);
        
        return dto;
    }
    
    public async Task<EquipmentDto> UpdateEquipment(EquipmentUpdateDto equipmentDto, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentRepository.GetOneAsync(c => c.Id == ObjectId.Parse(equipmentDto.Id), cancellationToken);

        this._mapper.Map(equipmentDto, equipment);
        
        var result = await _equipmentRepository.UpdateEquipmentAsync(equipment, cancellationToken);
        
        return _mapper.Map<EquipmentDto>(result);
    }
    
    public async Task<EquipmentDto> DeleteEquipment(string equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentRepository.GetOneAsync(c => c.Id == ObjectId.Parse(equipmentId), cancellationToken);

        if (equipment == null)
        {
            throw new Exception("Equipment was not found!");
        }

        await _equipmentRepository.DeleteFromDbAsync(equipment, cancellationToken);

        return _mapper.Map<EquipmentDto>(equipment);
    }

    public async Task<EquipmentDto> HideEquipment(string equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentRepository.GetOneAsync(c => c.Id == ObjectId.Parse(equipmentId), cancellationToken);

        if (equipment == null)
        {
            throw new Exception("Equipment was not found!");
        }

        await _equipmentRepository.DeleteAsync(equipment, cancellationToken);

        return _mapper.Map<EquipmentDto>(equipment);
    }
    
    public async Task<EquipmentDto> RevealEquipment(string equipmentId, CancellationToken cancellationToken)
    {
        var service = await _equipmentRepository.GetOneAsync(c => c.Id == ObjectId.Parse(equipmentId), cancellationToken);

        if (service == null)
        {
            throw new Exception("Equipment was not found!");
        }

        await _equipmentRepository.RevealEquipmentAsync(equipmentId, cancellationToken);

        return _mapper.Map<EquipmentDto>(service);
    }
    
    public async Task<PagedList<EquipmentDto>> GetEquipmentPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken)
    {
        var entities = await _equipmentRepository.GetPageAsync(pageNumber, pageSize,
            x=> x.ServiceId == ObjectId.Parse(serviceId), cancellationToken);
        var dtos = _mapper.Map<List<EquipmentDto>>(entities);
        var count = await _equipmentRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<EquipmentDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<PagedList<EquipmentDto>> GetVisibleEquipmentPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken)
    {
        var entities = await _equipmentRepository.GetPageAsync(pageNumber, pageSize, x=> x.IsDeleted==false
            && x.ServiceId == ObjectId.Parse(serviceId), cancellationToken);
        var dtos = _mapper.Map<List<EquipmentDto>>(entities);
        var count = await _equipmentRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<EquipmentDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<bool> GetStatus(string equipmentId, CancellationToken cancellationToken)
    {
        return await _sensorsRepository.GetStatus(equipmentId);
    }
}