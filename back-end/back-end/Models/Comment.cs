using System;
using System.Collections.Generic;

namespace back_end.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int VideoId { get; set; }

    public DateTime CommentDate { get; set; }

    public string CommentText { get; set; } = null!;

    public int CommentLikesCount { get; set; }

    public int CommentDislikesCount { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
