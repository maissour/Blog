using System.ComponentModel.DataAnnotations;

namespace Repository.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string? Image { get; set; }
        public string? VideoUrl { get; set; }
        public List<CategoryDto>? Categories { get; set; }
    }
}
