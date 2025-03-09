
namespace Domain.Models
{
    public class Category: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
