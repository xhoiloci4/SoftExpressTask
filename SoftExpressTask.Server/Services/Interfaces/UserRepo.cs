

using SoftExpressTask.Server.Database.Models;
using SoftExpressTask.Server.Utils;

namespace SoftExpressTask.Server.Services.Interfaces
{
    public interface UserRepo
    {
        public Task<(Failure?, User?)> getByUsername(string username);
        public Task<(Failure?, User?)> get(Guid id);
        public Task<(Failure?, User?)> add(User user);
        public (Failure?, User?) update(User user);
        public (Failure?, User?) remove(User user);

    }
}
