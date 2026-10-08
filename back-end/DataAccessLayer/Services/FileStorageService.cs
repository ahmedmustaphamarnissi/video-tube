using Amazon.S3;
using Amazon.S3.Model;
using DataAccessLayer.Configuration;

namespace DataAccessLayer.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly FilebaseOptions _options;

    public FileStorageService(IAmazonS3 s3Client, FilebaseOptions options)
    {
        _s3Client = s3Client;
        _options = options;
    }

    public string GetPresignedUrl(string objectKey)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _options.BucketName,
            Key = objectKey,
            Expires = DateTime.UtcNow.AddHours(1),
            Verb = HttpVerb.GET
        };

        return _s3Client.GetPreSignedURL(request);
    }
}
