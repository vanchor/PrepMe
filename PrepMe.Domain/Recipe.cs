using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.Domain
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        [Column(TypeName = "nvarchar(20)")]
        public string ImageUrl { get; set; } = "";
    }
}
