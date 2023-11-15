using MySqlConnector;
using System.Data;
using System.Windows.Forms;


namespace AccesoDatosArtepiel
{
    public class ConexionBD
    {
        private MySqlConnection _conexion;
        private string _cadenaConexion;

        public ConexionBD(string servidor, string baseDatos, string usuario, string password)
        {
            _cadenaConexion = $"Data Source={servidor};Database={baseDatos};User Id={usuario};Password={password};";
            _conexion = new MySqlConnection(_cadenaConexion);
        }

        public bool AbrirConexion()
        {
            try
            {
                _conexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al abrir la conexión a la base de datos: " + ex.Message, "Error");
                return false;
            }
        }

        public bool CerrarConexion()
        {
            try
            {
                _conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cerrar la conexión a la base de datos: " + ex.Message, "Error");
                return false;
            }
        }

        public DataTable Consulta(string query)
        {
            DataTable dt = new DataTable();
            if (this.AbrirConexion())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, _conexion);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error en la consulta: " + ex.Message, "Error");
                }
                finally
                {
                    this.CerrarConexion();
                }
            }
            return dt;
        }

        public bool EjecutarQuery(string query)
        {
            if (this.AbrirConexion())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, _conexion);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error");
                    return false;
                }
                finally
                {
                    this.CerrarConexion();
                }
            }
            return false;
        }
    }
}
