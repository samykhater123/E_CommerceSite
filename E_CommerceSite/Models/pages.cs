using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Models
{
    public class pages
    {
        internal static string Content;

        public int id { get; set; }
        [Required,MinLength(2,ErrorMessage ="minimum 2 leter")]
        public string titel { get; set; }
        
        public string slug { get; set; }
        [Required, MinLength(2, ErrorMessage = "minimum 2 leter")]
        public string contant { get; set; }
        public int sorting { get; set; }
    }
}
