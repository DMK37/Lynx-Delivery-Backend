namespace CourierAppBackend.Abstractions.Services;

public interface IFileService
{
    Task<string> SaveFile(IFormFile file);
}