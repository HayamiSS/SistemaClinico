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
    public partial class Informes : Form
    {
        public Informes()
        {
            InitializeComponent();
        }

        
        private void Informes_Load(object sender, EventArgs e)
        {
            label16.Text ="";
            lblrut.Text = "";
            lblappat.Text = "";
            lblapmat.Text = "";
            lbledad.Text = "";
            lblfolio.Text = "";
            lbldiag1.Text = "";
            lbldiag2.Text = "";
            lblorigen.Text = "";
            lblcodmed.Text = "";
            lblcodtec.Text = "";
            MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
            conex.Open();
            DataTable datos = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from resultados", conex);
            datos.Clear();
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            conex.Close();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            try
            {
               
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string sql = "select * from resultados where RUT =('" + tbrut.Text + "') and FECHADIAG=('" + tbfecha.Text + "');";
                MessageBox.Show(sql);
                //string sql2 = "SELECT * FROM pacientes where RUT = ('" + tbrut.Text + "')";
                MySqlCommand comando = new MySqlCommand(sql, conex);
                //MySqlCommand comando2 = new MySqlCommand(sql2, conex);
                MySqlDataReader read = comando.ExecuteReader();
                //MySqlDataReader read2 = comando2.ExecuteReader();
                string nombre, rut, edad, appat, apmat, num, origen, diag1, diag2, codmedico, codtecno,fechadiag;
                    if (read.Read())
                    {
                        num = (read["NUM"].ToString());
                        rut = (read["RUT"].ToString());
                        edad = (read["EDAD"].ToString());
                        fechadiag = (read["FECHADIAG"].ToString());
                        origen = (read["ORIGEN"].ToString());
                        diag1 = (read["DIAGNOSTICO_1"].ToString());
                        diag2 = (read["DIAGNOSTICO_2"].ToString());
                        codmedico = (read["CODMEDICO"].ToString());
                        codtecno = (read["CODTECNOLOGO"].ToString());
                        lblrut.Text = rut;
                        lbledad.Text = edad;
                        lblfolio.Text = num;
                        lblorigen.Text = origen;
                        lbldiag1.Text = diag1;
                        lbldiag2.Text = diag2;
                        lblcodmed.Text = codmedico;
                        lblcodtec.Text = codtecno;
                    }
                    //if (read2.Read())
                    {
                    //nombre = (read2["NOMBRE"].ToString());
                    //appat = (read2["APPAT"].ToString());
                    //apmat = (read2["APMAT"].ToString());
                    }
            }
            catch (Exception )
            {
                MessageBox.Show("Paciente no existe o no contiene historial medico\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu_principal mprin = new Menu_principal();
            this.Hide();
            mprin.Show();
            
            
        }

        private void buscar2_Click(object sender, EventArgs e)
        {
            try
            { 
                DataTable datos = new DataTable();
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string sql = "select * from pacientes where NOMBRE=('" + tbnombre.Text + "') and APPAT=('" + tbappat.Text + "') and APMAT=('" + tbapmat.Text + "');";
                MessageBox.Show(sql);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conex);
                MySqlCommand comando = new MySqlCommand(sql, conex);
                adapter.Fill(datos);
                dataGridView1.DataSource = datos;
                conex.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Paciente no existe o no contiene historial medico\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string fecha1 = tbfec1.Text, fecha2 = tbfec2.Text;

                string sql = "select * from resultados where FECHADIAG >= ('" +tbfec1.Text + "') and FECHADIAG <= ('" + tbfec2.Text +"')";

                MySqlCommand comando = new MySqlCommand(sql, conex);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conex);
                DataTable datos = new DataTable();
                datos.Clear();
                adapter.Fill(datos);
                dataGridView1.DataSource = datos;
                conex.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Paciente no existe o no contiene historial medico\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string sql = "select * from resultados where  NUM >=('" + tbnum1.Text + "') and NUM <=('" + tbnum2.Text + "')";
                MySqlCommand comando = new MySqlCommand(sql, conex);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conex);
                DataTable datos = new DataTable();
                datos.Clear();
                adapter.Fill(datos);
                dataGridView1.DataSource = datos;
                conex.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Paciente no existe o no contiene historial medico\n");
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }
    }
}
