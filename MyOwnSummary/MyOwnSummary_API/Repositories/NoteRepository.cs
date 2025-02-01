using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyOwnSummary_API.Data;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Repositories.IRepository;

namespace MyOwnSummary_API.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(Note note)
        {
            _context.Update<Note>(note);
            await Save();
        }

        public async Task UpdatePractice(Note note)
        {
            note.Repetition++;
            _context.Update<Note>(note);
            await Save();
        }
    }
}
