using Api.Models.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Api.Services
{
    public class TestService
    {
        public string ConnectionString { get; set; } = string.Empty;
        public void GetSome(int offset, int limit, string order, string search, bool descending)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM 2021-07 ORDER BY @order OFFSET @offset LIMIT @limit";
            using var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@order", order);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@limit", limit);

            using SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine(sqlQuery);
        }
    
    }
}
