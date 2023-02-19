using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.Domain
{       
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";

        public ICollection<Recipe> Recipes { get; set; }
        public List<ProductRecipe> ProductRecipes { get; set; }

        public Product()
        {

        }
        
        public Product(string name)
        {
            ProductName = name;
        }
    }
}
