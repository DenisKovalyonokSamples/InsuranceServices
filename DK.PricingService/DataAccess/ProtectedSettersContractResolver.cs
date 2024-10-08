using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace DK.PricingService.DataAccess;

public class ProtectedSettersContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);

        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            if (property != null)
            {
                var hasSetter = property.GetSetMethod(true) != null;
                prop.Writable = hasSetter;
            }
        }

        return prop;
    }
}
