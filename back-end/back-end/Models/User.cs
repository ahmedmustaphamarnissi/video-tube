using System;
using System.Collections.Generic;

namespace back_end.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? PictureKey { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
