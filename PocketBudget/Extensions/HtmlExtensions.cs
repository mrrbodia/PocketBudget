using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlString ToJson(this object obj, JsonSerializerSettings jsonSettings = null)
        {
            jsonSettings = jsonSettings ?? new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(obj, jsonSettings);
            return new HtmlString(json);
        }
    }
}