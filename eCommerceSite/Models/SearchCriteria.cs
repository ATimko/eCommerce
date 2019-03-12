using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    public class SearchCriteria
    {
        /// <summary>
        /// The partial matching product name
        /// </summary>

        //[RegularExpression("[A-Za-z]+")]
        public string Name { get; set; }

        public string Category { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The low price should be positive.")]
        [Display(Name = "Mininum Price")]
        public double? LowPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "High price should be positive.")]
        [Display(Name = "Maximum Price")]
        public double? HighPrice { get; set; }

        ///<summary>
        ///The product list results from the search criteria
        ///</summary>
        public List<Product> Products { get; set; }
    }
}
