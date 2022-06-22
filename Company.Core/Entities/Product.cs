namespace Company.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Perishable { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

}
