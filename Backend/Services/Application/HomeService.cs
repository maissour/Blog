using Repository;
using Repository.Dto;
using Services.Interfaces;
namespace Services.Application
{
    public class HomeService: IHomeService
    {
        private readonly HomeRepository homeData;
        public HomeService(HomeRepository _homeData)
        {
            homeData = _homeData;
        }

        public async Task<bool> CreateArticle(ArticleDto articleDto)
        {
            return await homeData.CreateArticle(articleDto);
        }

        public async Task<bool> CreateCategory(CategoryDto categoryDto)
        {
            return await homeData.CreateCategory(categoryDto);
        }

        public async Task<List<ArticleDto>> GetAllArticles()
        {
            return await homeData.GetAllArticles();
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            return await homeData.GetCategoryById(id);
        }

        public async Task<ArticleDto> GetArticleById(int id)
        {
            return await homeData.GetArticleById(id);
        }

        public async Task<List<ArticleDto>> GetTopFiveRecentArticle()
        {
            return await homeData.GetTopFiveRecentArticle();
        }
    }
}
