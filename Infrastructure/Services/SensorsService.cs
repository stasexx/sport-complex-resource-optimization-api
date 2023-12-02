using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class SensorsService : ISensorService
{
    private readonly ISensorsRepository _sensorsRepository;

    private readonly IUsagesHistoryRepository _usagesHistoryRepository;

    public SensorsService(ISensorsRepository sensorsRepository, IUsagesHistoryRepository usagesHistoryRepository)
    {
        _sensorsRepository = sensorsRepository;
        _usagesHistoryRepository = usagesHistoryRepository;
    }

    public async Task<SensorCreateDto> CreateSensor(SensorCreateDto dto, CancellationToken cancellationToken)
    {
        var sensor = new Sensor()
        {
            EquipmentId = ObjectId.Parse(dto.EquipmentId),
            Status = false
        };
        
        await _sensorsRepository.AddAsync(sensor, cancellationToken);

        return dto;
    }

    public async Task<SensorUpdateDto> UpdateStatus(SensorUpdateDto dto, CancellationToken cancellationToken)
    {
        await _sensorsRepository.UpdateStatus(dto.EquipmentId, dto.Status, cancellationToken);
        if (dto.Status)
        {
            await _usagesHistoryRepository.UpdateUsages(dto.EquipmentId);
        }
        
        return dto;
    }

    public async Task<Sensor> UpdateEquipment(string sensorId, string newEquipmentId, CancellationToken cancellationToken)
    {
        return await _sensorsRepository.UpdateEquipment(sensorId, newEquipmentId, cancellationToken);
    }
}