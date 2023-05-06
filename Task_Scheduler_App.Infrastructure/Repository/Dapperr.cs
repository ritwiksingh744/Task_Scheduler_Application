using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Task_Scheduler_App.Application.Repository.Interface;

namespace Task_Scheduler_App.Infrastructure.Repository
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private readonly string Connectionstring = "MyConnection";

        public Dapperr(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {
        }

        public async Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = GetDbconnection();
            var res = await db.ExecuteAsync(sp, parms, commandType: commandType);
            db.Close();
            return res;
        }

        public async Task<List<T>> QueryAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = GetDbconnection();
            var res = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            db.Close();
            return res.ToList();
        }

        private DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }
    }
}