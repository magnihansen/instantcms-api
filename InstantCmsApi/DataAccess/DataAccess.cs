using System.Data;
using Dapper;
using MySqlConnector;

namespace InstantCmsApi.DataAccess;

public class DataAccess : IDataAccess
{
    private readonly string? _connectionString;

    public DataAccess(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("ConnectionStrings:Default")?.ToString();
    }

    public async Task<T> LoadSingleData<T, U>(string sql, U parameters)
    {
        //using (StreamWriter file = new(_webHostEnvironment.WebRootPath + "LoadSingleData.txt", append: true))
        //{
        //    await file.WriteLineAsync(sql);
        //}

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            T data = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
            return data != null ? data : default;
        }
    }

    public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
    {
        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var rows = await connection.QueryAsync<T>(sql, parameters);
            return rows.AsList();
        }
    }

    public async Task<int> SaveData<T>(string sql, T parameters)
    {
        //using (StreamWriter file = new(_webHostEnvironment.WebRootPath + "SaveData.txt", append: true))
        //{
        //    await file.WriteLineAsync(sql);
        //}

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            int result = await connection.ExecuteAsync(sql, parameters);
            return result;
        }
    }

    public async Task<int> SaveDataWithReturn<T>(string sql, T parameters)
    {
        //using (StreamWriter file = new(_webHostEnvironment.WebRootPath + "SaveDataWithReturn.txt", append: true))
        //{
        //    await file.WriteLineAsync(sql);
        //}

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            int results = await connection.ExecuteScalarAsync<int>(sql, parameters);
            return results;
        }
    }
}
