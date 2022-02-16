using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity()
        {

        }

        public ProductEntity(string name, string descripton, int categoryId)
        {
            Name = name;
            Descripton = descripton;
            CategoryId = categoryId;
        }

        public ProductEntity(int id, string name, string descripton, int categoryId)
        {
            Id = id;
            Name = name;
            Descripton = descripton;
            CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descripton { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

    }
}
