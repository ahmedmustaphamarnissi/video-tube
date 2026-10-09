using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class CategoriesData : BaseData
{
    public CategoriesData(IConfiguration config) : base(config)
    {

    }

    public async Task<List<CategoriesDTO>> GetCategoriesAsync()
    {
        using var context = CreateDbContext();
        var categories = await context.Categories
            .Select(c => new CategoriesDTO
            {
                categoryId = c.Id,
                categoryName = c.Name,
                IconName = c.IconName
            })
            .ToListAsync();
        return categories;
    }
}
