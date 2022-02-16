namespace WebApi.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {

        }

        public CategoryModel(string name)
        {
            Name = name;
        }

        public CategoryModel(int id)
        {
            Id = id;
        }

        public CategoryModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
