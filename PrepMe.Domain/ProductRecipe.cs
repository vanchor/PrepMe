using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.Domain
{
    public class ProductRecipe
    {
        public double Gramms { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
