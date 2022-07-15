using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PROG2INTE_NICOLASTRA
{
    public partial class Menu_principal : Form
    {
        public Menu_principal()
        {
            InitializeComponent();
        }
        PACIENTES PACIENTES = new PACIENTES();
        Resultados RESULTADOS = new Resultados();
        Informes Informe = new Informes();
        
        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PACIENTES.Show();
            
            
        }

        private void resultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RESULTADOS.Show();
           
        }

        private void informeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Informe.Show();
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Menu_principal_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable tabla = new DataTable();
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string sql = ("Select * from usuarios");
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conex);
                tabla.Clear();
                adapter.Fill(tabla);
                int totalregnum = tabla.Rows.Count;
                for (int i = 0; i < totalregnum; i++)
                {
                    if (tabla.Rows[i]["nivel"].ToString() == "2")
                    {
                        pacientesToolStripMenuItem.Enabled = false;
                    }
                    else { pacientesToolStripMenuItem.Enabled = true; }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error xD");
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
