using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static IHtmlString FormatAsPrice(this decimal amount)
        {
            var regex = new Regex(@"(\d)(?=(\d\d\d)+(?!\d))");
            var str = regex.Replace(amount.ToString(), "$1,");
            return new HtmlString(str);
        }

        public static IHtmlString GetCurrencySymbol()
        {
            return new HtmlString(Constants.Currency.HrnSymbol);
        }
    }
}