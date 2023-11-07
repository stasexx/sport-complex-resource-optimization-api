namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface ISportComlexesService
{
    Task UploadImageToBlobStorage(Stream imageStream, string imageName);

    List<string> GetSportComplexImages(string sportComplexId);
}