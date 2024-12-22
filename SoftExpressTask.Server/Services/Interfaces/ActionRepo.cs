using SoftExpressTask.Server.Database.Models;
using SoftExpressTask.Server.Utils;


namespace SoftExpressTask.Server.Services.Interfaces
{
    public interface ActionRepo
    {
        public Task<(Failure?, Actionn?)> get(Guid id);
        public (Failure?, Actionn[]) getAll();
        public (Failure?, Actionn[]) getFiltered(string? region, string? devicetype, string? application, string? timedate);
        public Task<(Failure?, Actionn?)> add(Actionn action);
        public Task<(Failure?, Actionn?)> update(Actionn action);
        public Task<(Failure?, Actionn?)> delete(Actionn action);

    }
}
