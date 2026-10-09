using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class CommentsData : BaseData
{
    private readonly IFileStorageService _fileStorageService;

    public CommentsData(
        IConfiguration config,
        IFileStorageService fileStorageService) : base(config)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<List<CommentDTO>?> GetCommentsByVideoIdAsync(int videoId)
    {
        using var context = CreateDbContext();
        bool videoExists = await context.Videos.AnyAsync(v => v.Id == videoId);
        if (!videoExists)
        {
            return null;
        }

        var comments = await context.Comments
            .Where(c => c.VideoId == videoId)
            .Select(c => new CommentDTO
            {
                commentText = c.CommentText,
                commentDate = c.CommentDate,
                userName = c.User.UserName,
                userProfileUrl = c.User.PictureKey,
                commentLikes = c.CommentLikesCount,
                commentDislikes = c.CommentDislikesCount
            })
            .ToListAsync();

        return comments;
    }
}
