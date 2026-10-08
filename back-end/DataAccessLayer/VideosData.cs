using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer;

public class VideosData : BaseData
{
    public VideosData(IConfiguration config) : base(config)
    {

    }
}
