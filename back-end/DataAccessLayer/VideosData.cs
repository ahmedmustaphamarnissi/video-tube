
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Services;

namespace DataAccessLayer;

public class VideosData : BaseData
{
    private readonly IFileStorageService _fileStorageService;

    public VideosData(
        IConfiguration config,
        IFileStorageService fileStorageService) : base(config)
    {
        _fileStorageService = fileStorageService;
    }

    public Task<string> GetVideoPresignedUrlAsync(string objectKey)
    {
        return Task.FromResult(
            _fileStorageService.GetPresignedUrl(objectKey));
    }
}