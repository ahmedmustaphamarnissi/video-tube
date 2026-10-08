using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class CategoriesData : BaseData
{
    public CategoriesData(IConfiguration config) : base(config)
    {

    }
}
