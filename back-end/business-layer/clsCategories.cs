using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using Microsoft.Extensions.Configuration;

namespace business_layer;

public class clsCategories : BaseService
{
    public clsCategories(IConfiguration config) : base(config)
    {

    }

    public async Task<List<CategoriesDTO>> GetCategoriesAsync()
    {
        var _categoriesData = new DataAccessLayer.CategoriesData(_config);
        return await _categoriesData.GetCategoriesAsync();
    }
}
