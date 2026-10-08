using System;
using System.Collections.Generic;

namespace back_end.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string IconName { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
