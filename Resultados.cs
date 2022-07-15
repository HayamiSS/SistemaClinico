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
    public partial class Resultados : Form
    {
        public Resultados()
        {
            InitializeComponent();
        }

        
        private void Resultados_Load(object sender, EventArgs e)
        {
            DataTable datos = new DataTable();
            MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
            conex.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from resultados", conex);
            datos.Clear();
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            conex.Close();

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

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (comprobarUsuario(this.tbrut.Text))
            {
                try
                {
                    int numdoc = 0;
                    MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = conex;
                    conex.Open();
                    comando.CommandText = "Insert into resultados (NUM,RUT,EDAD,FECHADIAG,DIAGNOSTICO_1,DIAGNOSTICO_2,ORIGEN,CODMEDICO,CODTECNOLOGO ) values (@NUM,@RUT,@EDAD,@FECHADIAG,@DIAGNOSTICO_1,@DIAGNOSTICO_2,@ORIGEN,@CODMEDICO,@CODTECNOLOGO)";
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("NUM", tbnumero.Text);
                    comando.Parameters.AddWithValue("RUT", tbrut.Text);
                    comando.Parameters.AddWithValue("EDAD", tbedad.Text);
                    comando.Parameters.AddWithValue("FECHADIAG", tbfechadiag.Text);
                    comando.Parameters.AddWithValue("DIAGNOSTICO_1", cbdiag1.Text);
                    comando.Parameters.AddWithValue("DIAGNOSTICO_2", cbdiag2.Text);
                    comando.Parameters.AddWithValue("ORIGEN", cborigen.Text);
                    comando.Parameters.AddWithValue("CODMEDICO", cbcodmedico.Text);
                    comando.Parameters.AddWithValue("CODTECNOLOGO", cbcodtecno.Text);
                    

                    int nfilas = comando.ExecuteNonQuery();
                    if (nfilas > 0)
                    {
                        MessageBox.Show("Datos guardados correctamente,\n Numero de resultado: "+numdoc);
                    }
                    conex.Close();
                    comando.Dispose();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex + "Ha ocurrido un error, favor de verificar datos ingresados");
                }
                
            }

        }
        private bool comprobarUsuario(string Rut)
        {

            // Conectar a la base de datos
            MySqlConnection cnn = null;
            //
            try
            {

                cnn = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                cnn.Open();
                System.Text.StringBuilder sel = new System.Text.StringBuilder();
                sel.Append("SELECT COUNT(*) FROM pacientes ");
                sel.Append("WHERE Rut = @Rut");
                MySqlCommand cmd = new MySqlCommand(sel.ToString(), cnn);
                cmd.Parameters.Add("@Rut", MySqlDbType.VarChar, 50);
                cmd.Parameters["@Rut"].Value = Rut;
                int t = Convert.ToInt32(cmd.ExecuteScalar());
                cnn.Close();
                if (t == 0)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR, PACIENTE NO EXISTE, DEBE CREAR PACIENTE PARA GENERAR RESULTADOS: \n" +
                    ex.Message, "Comprobar usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Dispose();
                }
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //if (validarRut(tbrut.Text) == 1) 
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conex;
                conex.Open();
                comando.CommandText = "UPDATE resultados SET RUT = @RUT, EDAD = @EDAD, FECHADIAG = @FECHADIAG, DIAGNOSTICO_1 = @DIAGNOSTICO_1, DIAGNOSTICO_2 = @DIAGNOSTICO_2, ORIGEN = @ORIGEN, CODMEDICO = @CODMEDICO, CODTECNOLOGO = @CODTECNOLOGO WHERE NUM = @NUM";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("NUM", tbnumero.Text);
                comando.Parameters.AddWithValue("RUT", tbrut.Text);
                comando.Parameters.AddWithValue("EDAD", tbedad.Text);
                comando.Parameters.AddWithValue("FECHADIAG", tbfechadiag.Text);
                comando.Parameters.AddWithValue("DIAGNOSTICO_1", cbdiag1.Text);
                comando.Parameters.AddWithValue("DIAGNOSTICO_2", cbdiag2.Text);
                comando.Parameters.AddWithValue("ORIGEN", cborigen.Text);
                comando.Parameters.AddWithValue("CODMEDICO", cbcodmedico.Text);
                comando.Parameters.AddWithValue("CODTECNOLOGO", cbcodtecno.Text);
               /* MySqlDataReader read = comando.ExecuteReader();
                if (read.Read())
                {
                    tbnumero.Text = (read["NUM"].ToString());
                    tbrut.Text = (read["RUT"].ToString());
                    tbedad.Text = (read["EDAD"].ToString());
                    tbfechadiag.Text = (read["FECHADIAG"].ToString());
                    cbdiag1.Text = (read["DIAGNOSTICO_1"].ToString());
                    cbdiag2.Text = (read["DIAGNOSTICO_2"].ToString());
                    cborigen.Text = (read["ORIGEN"].ToString());
                    cbcodmedico.Text = (read["CODMEDICO"].ToString());
                    cbcodtecno.Text = (read["CODTECNOLOGO"].ToString());
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("ha ocurrido un error, " + ex);
            }
        }
        public void validarRut()
        {
            //el try & catch sirve para arrojar mensajitos de errores en caso de datos erroneos y asi no se cierra el programa
            // no estoy seguro si realmente valida el rut, en mi opinion prefiero usar el que todos sacamos de google.
            try
            {
                string rut = tbrut.Text;
                tbrut.Text = "";
                if (rut.Length == 9)
                {
                    rut = 0 + rut;
                }

                string[] arrestring = new string[10];
                for (int i = 0; i < 10; i++)
                {
                    arrestring[i] = rut[i].ToString();
                }
                int[] arreint = new int[8];
                for (int i = 0; i < 8; i++)
                {
                    arreint[i] = Int32.Parse(arrestring[i]);
                }
                double suma = 0, divisiondecimal = 0;
                suma = 3 * arreint[0] + 2 * arreint[1] + 7 * arreint[2] + 6 * arreint[3] + 5 * arreint[4] + 4 * arreint[5] + 3 * arreint[6] + 2 * arreint[7];
                int divisionentero = 0;
                divisiondecimal = suma / 11;
                divisionentero = (int)divisiondecimal;
                double valordecimal = 0;
                valordecimal = divisiondecimal - divisionentero;
                double resta11 = 0;
                resta11 = (11 - (11 * (valordecimal)));
                int digito = 0;
                digito = (int)resta11;
                bool comp = true;
                MessageBox.Show("Rut correcto");
            }
            catch (Exception)
            {
                bool comp = false;
                MessageBox.Show("Rut invalido");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable datos = new DataTable();
                MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                conex.Open();
                string sql = "delete from resultados where NUM ='" + tbnumero.Text + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conex);
                datos.Clear();
                adapter.Fill(datos);
                MySqlDataAdapter adapter2 = new MySqlDataAdapter("select * from resultados", conex);
                datos.Clear();
                adapter2.Fill(datos);
                dataGridView1.DataSource = datos;
                conex.Close();
                MySqlCommand comando = new MySqlCommand(sql, conex);
                MySqlDataReader read = comando.ExecuteReader();
                int totalregnum = datos.Rows.Count;
                for (int i = 0; i < totalregnum; i++)
                {
                    int a = 0;
                    if (datos.Rows[i]["NUM"].ToString() == tbnumero.Text)
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
            catch (Exception ex)
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu_principal mprin = new Menu_principal();
            mprin.Show();
            
            
        }
    }
}


