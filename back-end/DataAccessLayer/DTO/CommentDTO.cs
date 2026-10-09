using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO;

public class CommentDTO
{
    public string commentText { get; set; } = null!;
    public DateTime commentDate { get; set; }
    public string userName { get; set; } = null!;
    public string userProfileUrl { get; set; } = null!;
    public int commentLikes { get; set; }
    public int commentDislikes { get; set; }
}
