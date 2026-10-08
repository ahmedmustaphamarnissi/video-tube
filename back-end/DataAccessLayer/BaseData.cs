using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using back_end.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class BaseData
{
    protected readonly IConfiguration _config;

    public BaseData(IConfiguration config)
    {
        _config = config;
    }

    protected YouTubeCloneContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<YouTubeCloneContext>()
            .UseSqlServer(_config.GetConnectionString("DefaultConnection"))
            .Options;

        return new YouTubeCloneContext(options);
    }
}
