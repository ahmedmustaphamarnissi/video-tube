using System;
using System.Collections.Generic;

namespace back_end.Models;

public partial class Channel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? PictureKey { get; set; }

    public bool IsVerified { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
