
using Domain.Constants;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Data;
using Repository.Dto;

namespace Repository
{
    public class HomeRepository
    {
        private readonly ApplicationDbContext context;
        public HomeRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> CreateArticle(ArticleDto articleDto)
        {
            var exist = await context.Articles.FirstOrDefaultAsync(x => x.Slug == articleDto.Slug);
            if (exist != null) return false;
            var article = new Article
            {
                Title = articleDto.Title,
                Slug = articleDto.Slug,
                Description = articleDto.Description,
                Text = articleDto.Text,
                Image = articleDto.Image,
                VideoUrl = articleDto.VideoUrl,
                ArticleCategories = new List<ArticleCategory>()
            };

            await context.Articles.AddAsync(article);
            await context.SaveChangesAsync();
            if (!articleDto.Categories.IsNullOrEmpty())
            {
                foreach (var item in articleDto.Categories)
                {
                    article.ArticleCategories.Add(new ArticleCategory
                    {
                        ArticleId = article.Id,
                        CategoryId = item.Id
                    });
                }
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> CreateCategory(CategoryDto categoryDto)
        {
            var exist = await context.Categories.FirstOrDefaultAsync(x => x.Slug == categoryDto.Slug);
            if (exist != null) return false;
            var category = new Category
            {
                Name = categoryDto.Name,
                Slug = categoryDto.Slug
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ArticleDto>> GetAllArticles()
        {
            var articles = await context.Articles
                                        .Include(x => x.ArticleCategories)
                                        .ThenInclude(x => x.Category)
                                        .ToListAsync();
            if (articles.IsNullOrEmpty()) return null;
            var model = new List<ArticleDto>();
            foreach(var item in articles)
            {
                model.Add(new ArticleDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Slug = item.Slug,
                    Description = item.Description,
                    Text = item.Text,
                    Image = item.Image,
                    VideoUrl = item.VideoUrl,
                    Categories = item.ArticleCategories.Select(x => new CategoryDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                        Slug = x.Category.Slug
                    }).ToList()
                });
            }
            return model;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return null;
            var model = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
            return model;
        }

        public async Task<ArticleDto> GetArticleById(int id)
        {
            var article = await context.Articles
                                .Include(x => x.ArticleCategories)
                                .ThenInclude(x => x.Category)
                                .FirstOrDefaultAsync(x => x.Id == id);
            if (article == null) return null;
            var model = new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                Description = article.Description,
                Text = article.Text,
                Image = article.Image,
                VideoUrl = article.VideoUrl,
                Categories = article.ArticleCategories.Select(x => new CategoryDto
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                    Slug = x.Category.Slug
                }).ToList()
            };
            return model;
        }

        public async Task<List<ArticleDto>> GetTopFiveRecentArticle()
        {
            var articles = await context.Articles
                                        .Include(x => x.ArticleCategories)
                                        .ThenInclude(x => x.Category)
                                        .OrderByDescending(x => x.Id)
                                        .Take(GeneralConstants.RecentArticle)
                                        .ToListAsync();
            if (articles.IsNullOrEmpty()) return null;
            var model = new List<ArticleDto>();
            foreach (var item in articles)
            {
                model.Add(new ArticleDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Slug = item.Slug,
                    Description = item.Description,
                    Text = item.Text,
                    Image = item.Image,
                    VideoUrl = item.VideoUrl,
                    Categories = item.ArticleCategories.Select(x => new CategoryDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                        Slug = x.Category.Slug
                    }).ToList()
                });
            }
            return model;
        }
    }
}
