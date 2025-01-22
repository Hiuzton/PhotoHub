using Amazon.S3.Model;
using Amazon.S3;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly IConfiguration _configuration;

    public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
    {
        _s3Client = s3Client;
        _configuration = configuration;
    }

    public Task DeleteFileAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var bucketName = _configuration["AWS:BucketName"];
        var key = Guid.NewGuid() + Path.GetExtension(file.FileName);

        using var stream = file.OpenReadStream();
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(request);
        return $"https://{bucketName}.s3.amazonaws.com/{key}";
    }
}
