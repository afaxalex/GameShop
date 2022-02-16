namespace WebApi.Models
{
    public class ProductCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
