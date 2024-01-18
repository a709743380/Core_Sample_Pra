using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace Core_Sample_Pra.MyGear
{
    public class Gear
    {
        private IConfiguration configuration;
        public Gear(IConfiguration _Configuration)
        {
            configuration = _Configuration;
        }
        public string GetValue_From_Appsettings(string appsettings_Name)
        {
            string appsettings_Value = configuration.GetValue<string>(appsettings_Name);
            return appsettings_Value;
        }
        public T GetSectionValueToModel_From_Appsettings<T>(string appsettings_Node)
        {

            IConfigurationSection section = configuration.GetSection(appsettings_Node);
            var model = section.Get<T>();
            return model;

        }
        public string ListToJson(object model)
        {
            string jsonstring = JsonConvert.SerializeObject(model);
            return jsonstring;
        }
    }
}
