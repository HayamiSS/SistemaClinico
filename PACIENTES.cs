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
    public partial class PACIENTES : Form
    {
        public PACIENTES()
        {
            InitializeComponent();
        }

        
        private void PACIENTES_Load(object sender, EventArgs e)
        {
            DataTable datos = new DataTable();
            MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
            conex.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from pacientes", conex);
            datos.Clear();
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            conex.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           // if (validarRut(tbrut.Text)==) {
                try
                {
                    MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = conex;
                    conex.Open();
                    comando.CommandText = "Insert into pacientes (RUT, NOMBRE, APPAT, APMAT) values (@RUT, @NOMBRE, @APPAT, @APMAT)";

                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("RUT", tbrut.Text);
                    comando.Parameters.AddWithValue("NOMBRE", tbnombre.Text);
                    comando.Parameters.AddWithValue("APPAT", tbappat.Text);
                    comando.Parameters.AddWithValue("APMAT", tbapmat.Text);
                    //MySqlDataReader read = comando.ExecuteReader();
                    //conex.Close();
                    //string rut, nombre, apmat, appat;
                    //if (read.Read())
                //{
                    //rut = (read["RUT"].ToString());
                    //nombre = (read["NOMBRE"].ToString());
                    //appat = (read["APPAT"].ToString());
                    //apmat = (read["APMAT"].ToString());
                    //tbrut.Text = rut;
                    //tbnombre.Text = nombre;
                    //tbappat.Text = appat;
                    //tbapmat.Text = apmat;
                //}

                    int nfilas = comando.ExecuteNonQuery();
                    if (nfilas > 0)
                    {
                        MessageBox.Show("Datos guardados correctamente , que jebus te bendiga ;)");
                    }
                    
                    comando.Dispose();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex + "Ha ocurrido un error, favor de verificar datos ingresados");
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conex;
                conex.Open();
                comando.CommandText = "UPDATE pacientes SET NOMBRE = @NOMBRE, APPAT = @APPAT, APMAT = @APMAT WHERE RUT = @RUT";
         
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("RUT", tbrut.Text);
                comando.Parameters.AddWithValue("NOMBRE", tbnombre.Text);
                comando.Parameters.AddWithValue("APPAT", tbappat.Text);
                comando.Parameters.AddWithValue("APMAT", tbapmat.Text);
                /*MySqlDataReader read = comando.ExecuteReader();
                string rut, nombre, apmat, appat;
                if (read.Read())
                {
                    rut = (read["RUT"].ToString());
                    nombre = (read["NOMBRE"].ToString());
                    appat = (read["APPAT"].ToString());
                    apmat = (read["APMAT"].ToString());
                    tbrut.Text = rut;
                    tbnombre.Text = nombre;
                    tbappat.Text = appat;
                    tbapmat.Text = apmat;
                }*/
                
                int nfilas = comando.ExecuteNonQuery();
                if (nfilas > 0)
                {
                    MessageBox.Show("Datos guardados correctamente , que jebus te bendiga ;)");
                }
                conex.Close();
                comando.Dispose();
                //if (validarRut(tbrut.Text) == 1) 
                /*DataTable datos = new DataTable();
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                //string sql = "Update pacientes set APPAT =" + tbappat.Text + "and APMAT =" + tbapmat.Text + "Where RUT = " + tbrut.Text+";";
                string sql = "Update pacientes set APPAT = '" + tbappat.Text + "' AND APMAT= '" + tbapmat.Text + "' where RUT = ('" + tbrut.Text + "')";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conex);
                datos.Clear();
                adapter.Fill(datos);
                MySqlDataAdapter adapter2 = new MySqlDataAdapter("select * from pacientes", conex);
                datos.Clear();
                adapter2.Fill(datos);
                dataGridView1.DataSource = datos;
                //MySqlCommand comando = new MySqlCommand(sql, conex);
                //MySqlDataReader read = comando.ExecuteReader();
                conex.Close();
                //comando.ExecuteReader();
                //if (read.Read())
                //{

                  //  tbnombre.Text = (read["NOMBRE"].ToString());
                    //tbappat.Text = (read["APPAT"].ToString());
                    //tbapmat.Text = (read["APMAT"].ToString());
                //}
                
                
    */
            }
            catch (Exception ex)
            {
                MessageBox.Show("ha ocurrido un error, " + ex);
            }
        }
        public void validarRut(string text)
        {
            int salida = 0;
            if (salida == 0)
            {

                string rut = tbrut.Text;
                if (rut.Length > 10)
                {
                    rut = "0" + rut;
                }
                string[] arreRutString = new string[10];
                for (int i = 0; i < 10; i++)
                {
                    arreRutString[i] = rut[i].ToString();
                }
                int[] arreRutInt = new int[8];
                for (int i = 0; i < 8; i++)
                {
                    arreRutInt[i] = Int32.Parse(arreRutString[i]);
                }
                double suma = 0;
                double divicionDecimal = 0;
                suma = 3 * arreRutInt[0] + 2 * arreRutInt[1] + 7 * arreRutInt[2] + 6 * arreRutInt[3] +
                       5 * arreRutInt[4] + 4 * arreRutInt[5] + 3 * arreRutInt[6] + 2 * arreRutInt[7];
                int divicionEnteros = 0;
                divicionDecimal = suma / 11;
                divicionEnteros = (int)divicionDecimal;

                double valorDecimal = 0;
                valorDecimal = divicionEnteros - divicionDecimal;
                double resta11 = 0;
                resta11 = (11 - (11 * (valorDecimal)));
                int digito = 0;
                digito = (int)resta11;

                if (arreRutString[9] == "K" && digito == 10)
                {
                    MessageBox.Show("Rut Verificado Correctamente");
                    salida = 1;

                }
                else if (arreRutString[9] == "0" && digito == 11)
                {
                    MessageBox.Show("Rut Verificado Correctamente" + digito.ToString());
                    salida = 1;

                }
                else if (arreRutString[9] == digito.ToString())
                {
                    MessageBox.Show("Rut Verificado Correctamente");
                    salida = 1;

                }
                else
                {
                    MessageBox.Show("El Rut Esta incorrecto");
                    salida = 0;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable datos = new DataTable();
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string sql = "delete from pacientes where RUT ='" +tbrut.Text + "'";
                //string sql2 = "delete * from resultados where rut="+tbrut.Text+"'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql,conex);
                datos.Clear();
                adapter.Fill(datos);
                MySqlDataAdapter adapter2 = new MySqlDataAdapter("select * from pacientes", conex);
                datos.Clear();
                adapter2.Fill(datos);
                dataGridView1.DataSource = datos;
                
                //MySqlCommand comando = new MySqlCommand(sql, conex);
                //MySqlDataReader read = comando.ExecuteReader();
                int totalregnum = datos.Rows.Count;
                for (int i = 0; i < totalregnum;i++)
                {
                    int a = 0;
                    if (datos.Rows[i]["RUT"].ToString()== tbrut.Text)
                    {
                        a = 1;
                        MessageBox.Show("No se puede eliminar datos ");
                    }
                    else
                    {
                        a = 0;
                        MessageBox.Show("Datos eliminados");
                    }
                }
                conex.Close();
            }
            catch (Exception)
            {
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu_principal mprin = new Menu_principal();
            this.Hide();
            mprin.Show();
        }
    }
}