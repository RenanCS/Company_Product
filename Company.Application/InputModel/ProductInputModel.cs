using Company.Core.Entities;
using System.Text.Json.Serialization;

namespace Company.Application.InputModel
{
    public class ProductInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Perishable { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
