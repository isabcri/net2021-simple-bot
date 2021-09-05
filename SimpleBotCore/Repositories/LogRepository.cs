using System.Data.SqlClient;

namespace SimpleBotCore.Repositories
{
    public class LogRepository : ILogRepository
    {
        SqlConnection _connection;

        public LogRepository()
        {
            this._connection = new SqlConnection("Server=sqlServer:5050;Database=myDataBase;User Id=myUsername;");
        }

        public int TotalRegistros()
        {
            using (_connection)
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbLog", _connection);

                int total = (int)command.ExecuteScalar();
                _connection.Dispose();

                return total;
            }
        }

        public void CriarLog(string texto)
        {
            using (_connection)
            {
                SqlCommand sql = new SqlCommand("INSERT tbLog VALUES (@texto)");
                sql.Parameters.AddWithValue("@texto", texto);

                sql.ExecuteScalar();
            }
        }
    }
}
