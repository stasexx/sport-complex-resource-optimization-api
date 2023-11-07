namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IS3BucketService
{
    Task CreateSportComplexFolderIfNotExistsAsync(string sportComplexId);
}