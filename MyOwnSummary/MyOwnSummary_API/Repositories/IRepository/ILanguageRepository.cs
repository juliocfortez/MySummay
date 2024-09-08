using MyOwnSummary_API.Models;

namespace MyOwnSummary_API.Repositories.IRepository
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Task Update(Language language);
    }
}
