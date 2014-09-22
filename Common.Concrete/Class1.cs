using AutoMapper;
using Common.Services.Map;
using Common.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Concrete
{
    public class AutoMappingConfig : IRunAtInit
    {
        public void Execute()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            LoadStandardMapping(types);
            LoadCustomMapping(types);
        }

        private static void LoadStandardMapping(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
            }
        }
        private static void LoadCustomMapping(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMapping).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (ICustomMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}
