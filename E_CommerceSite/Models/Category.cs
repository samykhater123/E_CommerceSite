using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Models
{
    public class Category
    {

        public int id { get; set; }
        [Required, MinLength(2, ErrorMessage = "minimum 2 leter")]
        [RegularExpression(@"^[a-zA-Z-]+$",ErrorMessage ="only letares alowed.")]
        public string name { get; set; }
       
        public string slug { get; set; }
        public int sorting { get; set; }

    }
}
