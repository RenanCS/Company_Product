using Company.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.Application.InputModel
{

    public class CategoryInputModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public ICollection<Product> Products { get; set; }


    }
}
