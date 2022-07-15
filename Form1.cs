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
    public partial class nest : Form
    {
        public nest()
        {
            InitializeComponent();
        }

        
        Menu_principal mprin = new Menu_principal();
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
        private bool comprobarUsuario(string Rut, string clave)
        {

            // Conectar a la base de datos
            MySqlConnection cnn = null;
            //
            try
            {

                cnn = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
                cnn.Open();
                System.Text.StringBuilder sel = new System.Text.StringBuilder();
                sel.Append("SELECT COUNT(*) FROM usuarios ");
                sel.Append("WHERE Rut = @Rut AND Clave = @Clave");
                MySqlCommand cmd = new MySqlCommand(sel.ToString(), cnn);
                cmd.Parameters.Add("@Rut", MySqlDbType.VarChar, 50);
                cmd.Parameters.Add("@Clave", MySqlDbType.VarChar, 40);
                cmd.Parameters["@Rut"].Value = Rut;
                cmd.Parameters["@Clave"].Value = clave;
                int t = Convert.ToInt32(cmd.ExecuteScalar());
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                cnn.Close();
                //MySqlDataReader read = cmd.ExecuteReader();
                if (t == 0)
                {
                    return false;
                }
                /*if (read.Read())
                {
                    if ((read["nivel"].ToString()== "2"))
                    {
                        MessageBox.Show("Usuario solo puede verificar resultados de pacientes");
                    }

                }*/

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR al conectar a la base de datos: \n" +
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

        private void nest_Load(object sender, EventArgs e)
        {
            MySqlConnection conex = new MySqlConnection("server=127.0.0.1; user=root; database=bddlaboratorionicolastra; password='';");
            
            conex.Open();
            
        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            int veces = 0, NumeroIntentos = 4;
            if (comprobarUsuario(this.tbrut.Text, this.tbcontraseña.Text))
            {
                this.DialogResult = DialogResult.OK;
                mprin.Show();
                MessageBox.Show("wena ctm");

            }
            else
            {
                for (int i = 1; i <= NumeroIntentos; i++)
                {
                    if (veces <= NumeroIntentos)
                    {
                        veces = veces + 1;
                        MessageBox.Show("Quedan " + (NumeroIntentos - veces) + " intentos");
                        return;
                    }
                    this.DialogResult = DialogResult.No;
                }
            }
            Hide();
           
        }
        
        
    }
}