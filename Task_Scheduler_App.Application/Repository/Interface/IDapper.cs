using Dapper;
using System.Data;

namespace Task_Scheduler_App.Application.Repository.Interface
{
    public interface IDapper : IDisposable
    {
        Task<List<T>> QueryAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}