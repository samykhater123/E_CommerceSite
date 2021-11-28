using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Models
{
    public class product
    {
        public int id { get; set; }
        [Required, MinLength(2, ErrorMessage = "minimum 2 leter")]
        public string name { get; set; }

        public string slug { get; set; }

        [Required, MinLength(2, ErrorMessage = "minimum 2 leter")]
        public string discription { get; set; }
        [Column (TypeName ="decimal(18,2)")]
        public decimal price { get; set; }

        public string image { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="please choose the category")]
        public int categoryid { get; set; }
        [ForeignKey("categoryid")]
        public virtual Category category { get; set; }

        [NotMapped]
        public IFormFile imageuploade { get; set; }
    }
}
