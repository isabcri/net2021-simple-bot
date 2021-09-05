using System.Data.SqlClient;

namespace SimpleBotCore.Repositories
{
    public class LogRepository : ILogRepository
    {
        string _connectionString;

        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int TotalRegistros()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbLog", connection);

                int total = (int)command.ExecuteScalar();
                
                return total;
            }
        }

        public void CriarLog(string texto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand sql = new SqlCommand("INSERT tbLog VALUES (@texto)");
                sql.Parameters.AddWithValue("@texto", texto);

                sql.Connection = connection;

                sql.ExecuteScalar();
            }
        }
    }
}
