using ExercisesAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace ExercisesAPI.DAL.DAO
{
    public class UserDAO
    {
        private readonly AppDbContext _db;
        public UserDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public async Task<User> Register(User user)
        {
            await _db.Users!.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetByEmail(string? email)
        {
            User? user = await _db.Users!.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}