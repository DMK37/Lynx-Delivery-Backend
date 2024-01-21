using FirebaseAdmin;
using Google.Cloud.Storage.V1;

namespace CourierAppBackend.Services;

public class FileService : IFileService
{
    private readonly string _bucketName = "lynx-deliv.appspot.com";
    private readonly StorageClient _storageClient;

    public FileService()
    {
        FirebaseApp.Create(); // Initialize the Firebase app
        _storageClient = StorageClient.Create(); // Create a client to interact with Firebase Storage
    }
    
    public async Task<string> SaveFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        // Upload the file to Firebase Storage
        var firebaseObject = await _storageClient.UploadObjectAsync(
            _bucketName,
            file.FileName,
            null, // Specify the MIME type of the file here if you know it
            memoryStream
        );

        return firebaseObject.MediaLink; // Return the public URL of the uploaded file
    }
}