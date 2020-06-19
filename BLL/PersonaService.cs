using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL;
using Entity;
using System.Data;

namespace BLL
{
    public class PersonaService
    {
       
        private PersonaRepository PersonaRepository;
        private List<Persona> Personas;
        private SqlConnection SqlConnection;
        List<Persona> personas = new List<Persona>();

        public PersonaService()
        {
            SqlConnection = new SqlConnection(@"Data Source=DESKTOP-T64VO6F\\SQLEXPRESS; Initial Catalog=Persona;Integrated Security=True");
            PersonaRepository = new PersonaRepository(SqlConnection);
        }

        public string GuardarBD(Persona persona)
        {
            try
            {
                SqlConnection.Open();
                //LiquidacionRepository.GuardarBD(persona);
                SqlConnection.Close();
                return "Registro exitoso ";
            }
            catch (Exception e)
            {
                SqlConnection.Close();
                return "Registro fallido" + e.StackTrace.ToString();
            }
        }
        public List<Persona> Consultar(String ruta)
        { 
            return PersonaRepository.Consultar(ruta);
        }
    }
}
