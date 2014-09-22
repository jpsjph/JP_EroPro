using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Map
{
    public interface ICustomMapping
    {
        void CreateMappings(IConfiguration configuration);
    }
}
