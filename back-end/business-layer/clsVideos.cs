using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace business_layer;

public class clsVideos : BaseService
{
    public clsVideos(IConfiguration config) : base(config)
    {

    }
}
