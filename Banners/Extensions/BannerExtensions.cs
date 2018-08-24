using Banners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Banners.Extensions
{
    public static class BannerExtensions
    {
        public static async Task<List<string>> ValidateHtmlAsync(this Banner banner)
        {
            var errors = await Validator.W3Validator(banner.Html);

            return errors;
        }

    }
}
