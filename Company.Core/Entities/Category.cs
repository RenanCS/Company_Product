using System.Collections.Generic;

namespace Company.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
