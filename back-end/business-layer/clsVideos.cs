using DataAccessLayer.Configuration;
using DataAccessLayer.Services;
using Microsoft.Extensions.Configuration;

namespace business_layer;

public class clsVideos : BaseService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly FilebaseOptions _options;

    public clsVideos(
        IConfiguration config,
        FilebaseOptions options,
        IFileStorageService fileStorageService)
        : base(config)
    {
        _options = options;
        _fileStorageService = fileStorageService;
    }

    public Task<string> GetVideoPresignedUrlAsync(string objectKey)
    {
        var videosData = new DataAccessLayer.VideosData(
            _config,
            _fileStorageService);

        return videosData.GetVideoPresignedUrlAsync(objectKey);
    }
}