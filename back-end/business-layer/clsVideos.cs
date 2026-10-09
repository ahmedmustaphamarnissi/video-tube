using DataAccessLayer.Configuration;
using DataAccessLayer.DTO;
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

    public async Task<List<VideoDTO>> GetHomeVideosAsync(int pageNumber, int pageSize)
    {
        var videosData = new DataAccessLayer.VideosData(
            _config,
            _fileStorageService);
        return await videosData.GetHomeVideosAsync(pageNumber, pageSize);
    }

    public async Task<List<VideoDTO>?> GetVideosByCategoryAsync(int categoryId, int pageNumber, int pageSize)
    {
        var videosData = new DataAccessLayer.VideosData(
            _config,
            _fileStorageService);
        return await videosData.GetVideosByCategoryAsync(categoryId, pageNumber, pageSize);
    }

    public async Task<VideoDetailsDTO?> GetVideoDetailsAsync(int videoId)
    {
        var videosData = new DataAccessLayer.VideosData(
            _config,
            _fileStorageService);
        return await videosData.GetVideoDetailsAsync(videoId);
    }
    public Task<string> GetVideoPresignedUrlAsync(string objectKey)
    {
        var videosData = new DataAccessLayer.VideosData(
            _config,
            _fileStorageService);

        return videosData.GetVideoPresignedUrlAsync(objectKey);
    }
}