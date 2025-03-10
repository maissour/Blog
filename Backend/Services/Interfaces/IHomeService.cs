
using Repository.Dto;

namespace Services.Interfaces
{
    public interface IHomeService
    {
        Task<bool> CreateArticle(ArticleDto articleDto);
        Task<bool> CreateCategory(CategoryDto categoryDto);
        Task<List<ArticleDto>> GetAllArticles();
        Task<CategoryDto> GetCategoryById(int id);
        Task<ArticleDto> GetArticleById(int id);
    }
}
