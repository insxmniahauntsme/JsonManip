using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data{

    public class DataDapper{

        private IConfiguration _config;
        private string? _connectionString;
        public DataDapper(IConfiguration config){
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
        // private string _connectDb = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public IEnumerable<T> LoadData<T>(string pull_request){
            IDbConnection dbConnection = new SqlConnection(_connectionString);

            return dbConnection.Query<T>(pull_request);
        }

        public T LoadDataSingle<T>(string pull_request){
            IDbConnection dbConnection = new SqlConnection(_connectionString);

            return dbConnection.QuerySingle<T>(pull_request);
        }

        public int ExecuteSql(string pull_request){
            IDbConnection dbConnection = new SqlConnection(_connectionString);

            return dbConnection.Execute(pull_request);
        }

        public IDataReader SelectSql(string select_request){
            IDbConnection dbConnection = new SqlConnection(_connectionString);

            var result = dbConnection.ExecuteReader(select_request);

            return result;
        }

        public void ReadTable(IDataReader obj){
            while (obj.Read())
            {
                for (int i = 0; i < obj.FieldCount; i++)
                {
                    Console.WriteLine(obj[i].ToString());
                }
                Console.WriteLine();
            }
        }
    }
}

