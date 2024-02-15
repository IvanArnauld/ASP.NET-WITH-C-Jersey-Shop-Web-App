using ExercisesAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace ExercisesAPI.DAL.DAO
{
    public class CategoryDAO
    {
        private readonly AppDbContext _db;
        public CategoryDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _db.Categories!.ToListAsync();
        }
    }
}