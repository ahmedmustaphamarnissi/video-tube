using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO;

public class VideoDTO
{
    public int videoId { get; set; }
    public string title { get; set; } = null!;
    public int duration { get; set; }
    public DateTime uploadDate { get; set; }
    public int views { get; set; }
    public string thumbnailUrl { get; set; } = null!;
    public string channelName { get; set; } = null!;
    public string channelProfileUrl { get; set; } = null!;
    public bool isVerified { get; set; }

}
