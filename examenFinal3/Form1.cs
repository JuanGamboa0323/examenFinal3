using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;

namespace examenFinal3
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Llenar_Tabla()
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TablaDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            var ruta = openFile.FileName;
            MessageBox.Show(ruta);
            PersonaService personaServicio = new PersonaService(SqlConnection.sqlConnection);
            TablaDatos.DataSource = null;
            TablaDatos.DataSource = personaServicio.Consultar(ruta);
            string mensaje = personaServicio.GuardarBD(persona);
            

        }
    }
}
