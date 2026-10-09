using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO;

public class CategoriesDTO
{
    public int categoryId { get; set; }
    public string categoryName { get; set; } = null!;
    public string? IconName { get; set; }
}
