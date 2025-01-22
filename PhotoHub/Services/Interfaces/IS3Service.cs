
public interface IS3Service
{
    Task DeleteFileAsync(string fileName);
    Task<string> UploadFileAsync(IFormFile file);
}