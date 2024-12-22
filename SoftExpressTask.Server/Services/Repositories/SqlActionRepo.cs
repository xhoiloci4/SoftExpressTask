using SoftExpressTask.Server.Database;
using SoftExpressTask.Server.Database.Models;
using SoftExpressTask.Server.Services.Interfaces;
using SoftExpressTask.Server.Utils;


namespace SoftExpressTask.Server.Services.Repositories
{
    public class SqlActionRepo : ActionRepo
    {

        public AppDbContext _context { get; set; }


        public SqlActionRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(Failure?, Actionn?)> add(Actionn action)
        {
            try
            {

                var act = await _context.AddAsync(action);


                if (act == null) throw new Exception("Something went wrong");

                _context.SaveChanges();

                return (null, act.Entity);

            }
            catch (Exception ex)
            {
                return (new Failure(500, ex.Message), null);

            }
        }

        public Task<(Failure?, Actionn?)> delete(Actionn action)
        {
            throw new NotImplementedException();
        }

        public (Failure?, Actionn[]) getAll()
        {

            return (null, _context.Actions.ToArray());
        }

        public Task<(Failure?, Actionn?)> get(Guid id)
        {
            throw new NotImplementedException();
        }



        public Task<(Failure?, Actionn?)> update(Actionn action)
        {
            throw new NotImplementedException();
        }


        public (Failure?, Actionn[]) getFiltered(string? region, string? devicetype, string? application, string? timedate)
        {
            var query = _context.Actions.AsQueryable();

            // Filter by LlojiPajisjes
            if (!string.IsNullOrEmpty(devicetype))
            {
                query = query.Where(a => a.DeviceType == devicetype);
            }

            // Filter by Rajoni
            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(a => a.Region == region);
            }

            // Filter by Aplikacioni
            if (!string.IsNullOrEmpty(application))
            {
                query = query.Where(a => a.Application == application);
            }

            // Filter by DataOra (ensure it's parsed as DateTime)
            if (!string.IsNullOrEmpty(timedate) && DateTime.TryParse(timedate, out DateTime parsedDate))
            {
                query = query.Where(a => a.Timedate >= parsedDate);
            }

            // Execute the query
            var actions = query.ToArray();

            return (null, actions);
        }
    }
}
