using Banners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banners.Extensions
{
    public static class BannerExtensions
    {
        public static bool HasValidHtml(this Banner banner)
        {
            return true;
        }
    }
}
