namespace CourierAppBackend.Services;

public interface IFileService
{
    Task<string> SaveFile(IFormFile file);
}