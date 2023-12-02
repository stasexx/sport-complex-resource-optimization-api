using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface ISensorsRepository : IBaseRepository<Sensor>
{
    Task<bool> GetStatus(string equipmentId);

    Task<bool> UpdateStatus(string sensorId, bool newStatus, CancellationToken cancellationToken);

    Task<Sensor> UpdateEquipment(string sensorId, string newEquipmentId, CancellationToken cancellationToken);
}