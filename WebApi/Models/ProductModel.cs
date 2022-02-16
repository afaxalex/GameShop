namespace WebApi.Models
{
    public class ProductModel
    {
        public ProductModel()
        {

        }

        public ProductModel(string name, string description, CategoryModel category)
        {
            Name = name;
            Description = description;
            Category = category;
        }

        public ProductModel(int id, string name, string description, CategoryModel category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryModel Category { get; set; }
    }
}
