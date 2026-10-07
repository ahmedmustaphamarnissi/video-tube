using System;
using System.Collections.Generic;

namespace back_end.Models;

public partial class Video
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ChannelId { get; set; }

    public int CategoryId { get; set; }

    public int ViewsCount { get; set; }

    public DateTime AdditionDate { get; set; }

    public int LikesCount { get; set; }

    public int DislikesCount { get; set; }

    public string? Description { get; set; }

    public string VideoKey { get; set; } = null!;

    public string ThumbnailKey { get; set; } = null!;

    public int DurationSeconds { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Channel Channel { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
