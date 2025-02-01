using MyOwnSummary_API.Models;
namespace MyOwnSummary_API.Repositories.IRepository
{
    public interface INoteRepository : IRepository<Note>
    {
        Task Update(Note note);

        Task UpdatePractice(Note note);

    }
}
