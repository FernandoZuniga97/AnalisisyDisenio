using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MyWinFormsApp.Database
{
    public class DbConfig
    {
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static string xaa()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return "Conexión a Gicellimp exitosa.";
                }
            }
            catch (SqlException ex)
            {
                return $"Error de Conexión SQL: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error general de Conexión: {ex.Message}";
            }
        }
    }
}
