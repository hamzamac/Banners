using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banners.Models
{
    public class Banner
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Html { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
