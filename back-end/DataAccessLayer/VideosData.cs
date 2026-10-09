
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Services;
using DataAccessLayer.DTO;
using Microsoft.EntityFrameworkCore;

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


    public async Task<List<VideoDTO>> GetHomeVideosAsync(int pageNumber, int pageSize)
    {
        using var context = CreateDbContext();

        var videos = await context.Videos
            .OrderByDescending(v => v.ViewsCount)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VideoDTO
            {
                videoId = v.Id,
                title = v.Title,
                duration = v.DurationSeconds,
                uploadDate = v.AdditionDate,
                views = v.ViewsCount,
                thumbnailUrl = _fileStorageService.GetPresignedUrl(v.ThumbnailKey),
                channelName = v.Channel.Name,
                channelProfileUrl = _fileStorageService.GetPresignedUrl(v.Channel.PictureKey),
                isVerified = v.Channel.IsVerified
            })
            .ToListAsync();

        return videos;
    }

    public async Task<List<VideoDTO>?> GetVideosByCategoryAsync(int categoryId, int pageNumber, int pageSize)
    {
        using var context = CreateDbContext();

        bool IsThereCategory = await context.Categories.AnyAsync(c => c.Id == categoryId);

        if (!IsThereCategory)
            return null;

        var videos = await context.Videos.Where(v => v.CategoryId == categoryId)
            .OrderByDescending(v => v.ViewsCount)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VideoDTO
            {
                videoId = v.Id,
                title = v.Title,
                duration = v.DurationSeconds,
                uploadDate = v.AdditionDate,
                views = v.ViewsCount,
                thumbnailUrl = _fileStorageService.GetPresignedUrl(v.ThumbnailKey),
                channelName = v.Channel.Name,
                channelProfileUrl = _fileStorageService.GetPresignedUrl(v.Channel.PictureKey),
                isVerified = v.Channel.IsVerified
            })
            .ToListAsync();

        return videos;
    }

    public async Task<VideoDetailsDTO?> GetVideoDetailsAsync(int videoId)
    {
        using var context = CreateDbContext();
        var video = await context.Videos
            .Where(v => v.Id == videoId)
            .Select(v => new VideoDetailsDTO
            {
                videoId = v.Id,
                title = v.Title,
                duration = v.DurationSeconds,
                uploadDate = v.AdditionDate,
                views = v.ViewsCount,
                likesCount = v.LikesCount,
                dislikesCount = v.DislikesCount,
                description = v.Description,
                videoUrl = _fileStorageService.GetPresignedUrl(v.VideoKey),
                thumbnailUrl = _fileStorageService.GetPresignedUrl(v.ThumbnailKey),
                channelName = v.Channel.Name,
                channelProfileUrl = _fileStorageService.GetPresignedUrl(v.Channel.PictureKey),
                isVerified = v.Channel.IsVerified
            })
            .FirstOrDefaultAsync();
        return video;
    }
    public Task<string> GetVideoPresignedUrlAsync(string objectKey)
    {
        return Task.FromResult(
            _fileStorageService.GetPresignedUrl(objectKey));
    }
}