using Microsoft.EntityFrameworkCore;

using SoftExpressTask.Server.Database;
using SoftExpressTask.Server.Database.Models;
using SoftExpressTask.Server.Services.Interfaces;
using SoftExpressTask.Server.Utils;

namespace SoftExpressTask.Server.Services.Repositories
{
    public class SqlUserRepo : UserRepo
    {

        public AppDbContext _context { get; set; }


        public SqlUserRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(Failure?, User?)> add(User user)
        {
            try
            {

                var dbUser = await _context.AddAsync(user);


                if (dbUser == null) throw new Exception("Something went wrong");

                _context.SaveChanges();

                return (null, dbUser.Entity);

            }
            catch (Exception ex)
            {
                return (new Failure(500, ex.Message), null);

            }


        }

        public async Task<(Failure?, User?)> get(Guid id)
        {
            try
            {


                var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);


                if (dbUser == null)
                {
                    var failure = new Failure(404, "User not found");
                    return (failure, null);
                }

                return (null, dbUser);

            }
            catch (Exception ex)
            {
                return (new Failure(500, ex.Message), null);

            }
        }

        public (Failure?, User?) remove(User user)
        {
            try
            {

                var dbUser = _context.Remove(user);


                if (dbUser == null) throw new Exception("Something went wrong");
                _context.SaveChanges();

                return (null, dbUser.Entity);

            }
            catch (Exception ex)
            {
                return (new Failure(500, ex.Message), null);

            }
        }

        public (Failure?, User?) update(User user)
        {
            try
            {

                var dbUser = _context.Update(user);


                if (dbUser == null) throw new Exception("Something went wrong");
                _context.SaveChanges();

                return (null, dbUser.Entity);

            }
            catch (Exception ex)
            {
                return (new Failure(500, ex.Message), null);

            }

        }

        public async Task<(Failure?, User?)> getByUsername(string username)
        {
            try
            {


                var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);


                if (dbUser == null)
                {
                    var failure = new Failure(404, "User not found");
                    return (failure, null);
                }

                return (null, dbUser);

            }
            catch (Exception ex)
            {
                return (new Failure(500, ex.Message), null);

            }
        }
    }
}
