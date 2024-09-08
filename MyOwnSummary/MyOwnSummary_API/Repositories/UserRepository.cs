using Microsoft.EntityFrameworkCore;
using MyOwnSummary_API.Data;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Repositories.IRepository;

namespace MyOwnSummary_API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(User user)
        {
            _context.Update<User>(user);
            await Save();
        }

        public async Task<List<Language>?> GetLanguagesByUser(int userId)
        {
            var user = await _context.Users.Include(x=>x.Languages).ThenInclude(x=>x.Language).FirstOrDefaultAsync(x=>x.Id == userId);
            var languages = user == null ? null : user.Languages.Where(x=>x.UserId == userId).Select(x=>x.Language).ToList();
            return languages;
        }
    }
}
