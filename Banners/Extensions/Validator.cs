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
        //Consume W3 validator API to validate HTML document
        public static async Task<List<string>> W3Validator(string html)
        {
            using (var datastream = new MemoryStream())
            {
                // prepare the payload content
                var content = new StringContent(html);
                content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                
                var client = new HttpClient();

                //add headers to the client
                string header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!client.DefaultRequestHeaders.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                //initialise a Post request and collect the response
                var response = await client.PostAsync(@"https://validator.w3.org/nu/?out=json", content);

                var result = await response.Content.ReadAsStringAsync();

                //convert respon data to JSON object and extract all messages
                var messages = JObject.Parse(result).First.Values().ToList();

                //get error messages from messages of with type erroe
                var errors = messages.Where(m => m.Value<string>("type") == "error").Select(e => e.Value<string>("message")).ToList();

                return errors;
            }

        }
    }
}
