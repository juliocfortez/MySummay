using MyOwnSummary_API.Models;

namespace MyOwnSummary_API.Repositories.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task Update(Category category);
    }
}
