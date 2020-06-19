using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;            
using Entity;
using System.IO;
using System.Data;


namespace DAL
{
    
    public class PersonaRepository
    {

        private SqlConnection SqlConnection;
        private SqlDataReader SqlDataReader;
        List<Persona> personas = new List<Persona>();
        string ruta = @"valer.txt";

        public PersonaRepository(SqlConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }
        public List<Persona> Consultar(String rutafile)
        {
            
            
            FileStream archivo = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(archivo);

            string linea = string.Empty;

            while ((linea = reader.ReadLine()) != null)
            {

                Persona persona = new Persona();
                char Delimitar = ';';
                string[] ListaDatos = linea.Split(Delimitar);
                persona = new Persona();
                persona.Codigo = int.Parse(ListaDatos[0]);
                persona.Identificacion = ListaDatos[1];
                persona.Nombre = ListaDatos[2];
                persona.Fecha = DateTime.Parse(ListaDatos[3]);
                persona.Valor = decimal.Parse(ListaDatos[4]);

                personas.Add(persona);
            }
            reader.Close();
            archivo.Close();
            return personas;
        }


        public void GuardarBD(Persona persona)
        {
            using (var SqlCommand = SqlConnection.CreateCommand())
            {
                SqlCommand.CommandText = "Insert Into persona(Codigo,Identificacion,Nombre,Fecha,Valor)values(@Codigo,@Identificacion,@Nombre,@Fecha,@Valor)";

                SqlCommand.Parameters.Add("@Codigo", SqlDbType.SmallInt).Value = persona.Codigo;
                SqlCommand.Parameters.Add("@Identificacion", SqlDbType.VarChar).Value = persona.Identificacion;
                SqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = persona.Nombre;
                SqlCommand.Parameters.Add("@Fecha", SqlDbType.SmallDateTime).Value = persona.Fecha;
                SqlCommand.Parameters.Add("@Valor", SqlDbType.Decimal).Value = persona.Valor;
                SqlCommand.ExecuteNonQuery();
            }
        }


        public DataSet ConsultarBD(string codigo, string anio, string mes)
        {
            DataSet datos;
            using (var command = SqlConnection.CreateCommand())
            {
                datos = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                command.CommandText = "Filtro";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = codigo;
                command.Parameters.Add("@ano", SqlDbType.VarChar).Value = anio;
                command.Parameters.Add("@mes", SqlDbType.VarChar).Value = mes;
                da.SelectCommand = command;
                da.Fill(datos);
                command.Dispose();
            }
            return datos;
        }

        public Persona Map(SqlDataReader sqlDataReader)
        {
            Persona Persona = new Persona();
            Persona.Codigo = sqlDataReader.GetInt32(0);
            Persona.Identificacion = sqlDataReader.GetString(1);
            Persona.Nombre = sqlDataReader.GetString(2);
            Persona.Fecha = sqlDataReader.GetDateTime(3);
            Persona.Valor = sqlDataReader.GetDecimal(4);
            return Persona;
        }
    }
}
