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

    public SensorsService(ISensorsRepository sensorsRepository)
    {
        _sensorsRepository = sensorsRepository;
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
        await _sensorsRepository.UpdateStatus(dto.Id, dto.Status, cancellationToken);

        return dto;
    }
}