using System.Security.Cryptography;
using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DAO;
using ExercisesAPI.DAL.DomainClasses;
using ExercisesAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExercisesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        readonly AppDbContext _db;
        public RegisterController(AppDbContext context)
        {
            _db = context;
        }
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<UserHelper>> Index(UserHelper helper)
        {
            UserDAO dao = new(_db);
            User? already = await dao.GetByEmail(helper.Email);
            if (already == null)
            {
                HashSalt hashSalt = GenerateSaltedHash(64, helper.Password!);
                helper.Password = ""; // don’t need the string anymore
                User dbUser = new();
                dbUser.Firstname = helper.Firstname!;
                dbUser.Lastname = helper.Lastname!;
                dbUser.Email = helper.Email!;
                dbUser.Hash = hashSalt.Hash!;
                dbUser.Salt = hashSalt.Salt!;
                dbUser = await dao.Register(dbUser);
                if (dbUser.Id > 0)
                    helper.Token = "user registered";
                else
                    helper.Token = "user registration failed";
            }
            else
            {
                helper.Token = "user registration failed - email already in use";
            }
            return helper;
        }
        private static HashSalt GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = RandomNumberGenerator.Create();
            // Fills an array of bytes with a cryptographically strong sequence of random nonzero values.
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);
            // a password, salt, and iteration count, then generates a binary key
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            HashSalt hashSalt = new() { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }
    }
    public class HashSalt
    {
        public string? Hash { get; set; }
        public string? Salt { get; set; }
    }
}
