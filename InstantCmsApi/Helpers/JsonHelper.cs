using Newtonsoft.Json;

namespace InstantCmsApi.Helpers;

public static class JsonHelper
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}
