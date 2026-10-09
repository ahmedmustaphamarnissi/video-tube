using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Configuration;
using DataAccessLayer.DTO;
using DataAccessLayer.Services;
using Microsoft.Extensions.Configuration;

namespace business_layer;

public class clsComments : BaseService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly FilebaseOptions _options;

    public clsComments(
        IConfiguration config,
        FilebaseOptions options,
        IFileStorageService fileStorageService)
        : base(config)
    {
        _options = options;
        _fileStorageService = fileStorageService;
    }

    public async Task<List<CommentDTO>?> GetCommentsByVideoIdAsync(int videoId)
    {
        var commentsData = new DataAccessLayer.CommentsData(
            _config,
            _fileStorageService);
        return await commentsData.GetCommentsByVideoIdAsync(videoId);
    }
}
