using MyOwnSummary_API.Data;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Repositories.IRepository;

namespace MyOwnSummary_API.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Language language)
        {
            _context.Update<Language>(language);
            await Save();
        }
    }
}
