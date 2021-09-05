using Microsoft.AspNetCore.Mvc;
using SimpleBotCore.Models;
using System.Data.SqlClient;

namespace SimpleBotCore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            string commandText = "SELECT username FROM tbLogin WHERE email=@p1 AND pwd=@p2";

            using (SqlConnection connection = new SqlConnection("Server=.;Database=sql20;Integrated Security=SSPI"))
            {
                SqlCommand cmd = new SqlCommand(commandText, connection);
                cmd.Parameters.AddWithValue("@p1", model.Email ?? "");
                cmd.Parameters.AddWithValue("@p2", model.Password ?? "");

                connection.Open();
                object retorno = cmd.ExecuteScalar();

                if (retorno != null)
                {
                    string username = retorno.ToString();

                    return Redirect($"/Home/Dashboard?name={username}");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}