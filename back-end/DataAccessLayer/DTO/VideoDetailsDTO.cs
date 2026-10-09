using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO;

public class VideoDetailsDTO : VideoDTO
{
    public int likesCount { get; set; }
    public int dislikesCount { get; set; }
    public string? description { get; set; }
    public string? videoUrl { get; set; }
}
