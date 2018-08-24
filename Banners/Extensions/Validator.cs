using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Banners.Extensions
{
    public class Validator
    {
        public static async Task<List<string>> W3Validator(string html)
        {
            StringContent content;
            using (var datastream = new MemoryStream())
            {
                //using (var writer = new StreamWriter(datastream))
                //{
                //    writer.Write(html);
                    content = new StringContent(html);
                    content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                    string header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

                    var client = new HttpClient();
                    if (!client.DefaultRequestHeaders.UserAgent.TryParseAdd(header))
                    {
                        throw new Exception("Invalid header value: " + header);
                    }

                    var response = await client.PostAsync(@"https://validator.w3.org/nu/?out=json", content);

                    var result = await response.Content.ReadAsStringAsync();

                    var messages = JObject.Parse(result).First.Values().ToList();
                    var errors = messages.Where(m => m.Value<string>("type") == "error").Select(e => e.Value<string>("message")).ToList();

                    //var message = errors.Select(e => e.Value<string>("message")).ToList();

                    return errors;

                //}
            }

        }
    }
}
