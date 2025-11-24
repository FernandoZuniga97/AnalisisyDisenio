using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Windows.Forms;

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
        // -----------------------------------------------------------------
        // Hash SHA256
        // -----------------------------------------------------------------
        public static string HashPassword(string plain)
        {
            if (plain == null) return null;
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plain);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // -----------------------------------------------------------------
        // Registrar usuario (usa el SP RegistrarUsuario)
        // Retorna el Id nuevo (int > 0) o -1 si falla
        // -----------------------------------------------------------------
        public static int RegistrarUsuario(string username, string passwordPlain, string rol)
        {
            try
            {
                string passHash = HashPassword(passwordPlain);

                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("RegistrarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passHash);
                        cmd.Parameters.AddWithValue("@Rol", rol);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new Exception("RegistrarUsuario devolvió NULL.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en RegistrarUsuario:\n" + ex.Message, "BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }


        // -----------------------------------------------------------------
        // Validar login (usa SP ValidarLogin)
        // Devuelve un objeto anónimo con Id, Username, Rol, CodigoUnico si existe; null si no
        // -----------------------------------------------------------------
        public class AuthResult
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Rol { get; set; }
            public string CodigoUnico { get; set; }
        }

        public static AuthResult ValidarLogin(string username, string passwordPlain, string rol)
        {
            try
            {
                string passHash = HashPassword(passwordPlain);
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("ValidarLogin", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passHash);
                        cmd.Parameters.AddWithValue("@Rol", rol);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return new AuthResult
                                {
                                    Id = r.GetInt32(0),
                                    Username = r.IsDBNull(1) ? "" : r.GetString(1),
                                    Rol = r.IsDBNull(2) ? "" : r.GetString(2),
                                    CodigoUnico = r.IsDBNull(3) ? "" : r.GetString(3)
                                };
                            }
                        }
                    }
                }
            }
            catch
            {
                // log si lo deseas
            }
            return null;
        }




    }
}
