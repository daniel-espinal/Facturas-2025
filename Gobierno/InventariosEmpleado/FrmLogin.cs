using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using FacturacionGobierno.Properties;
namespace FacturacionGobierno
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }


        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            //Conexion vConexion = new Conexion();
            //this.DialogResult = DialogResult.OK;
            //if (txtUsuario.Text != "" && txtClave.Text != "")
            //{

            //    String vQuery = "[EEHGestiones_Login] '" + txtUsuario.Text + "', '" + txtClave.Text + "'";
            //    DataTable vDatos = new DataTable();
            //    vDatos = vConexion.obtenerDataTable1(vQuery);

            //    if (vDatos.Rows[0]["ID"].ToString() != "0")
            //    {
            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error en usuario", "Info");
            //        this.DialogResult = DialogResult.Retry;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Ingresar usuario/clave", "Info");
            //    this.DialogResult = DialogResult.Retry;
            //}

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            MD5 md5Hash = MD5.Create();
            string hashanterior = GetMd5Hash(md5Hash, txtClave.Text);
            string hashnuevo = GetMd5Hash(md5Hash, txtNuevaClave.Text);
            string select = String.Format("UPDATE FA_USUARIOS SET IDCLAVE = '{0}' WHERE IDUSUARIO = '{2}' AND IDCLAVE = '{1}'", hashnuevo, hashanterior, txtUsuario.Text);
            //Devart.Data.PostgreSql.PgSqlConnection Conexion = new PgSqlConnection(Settings.Default.FacturacionConnectionString);
            //Devart.Data.PostgreSql.PgSqlCommand Comando = new PgSqlCommand(select, Conexion);



            //   if (Conexion.State == 0)
            //       Conexion.Open();
            //int cuantos = Convert.ToInt32(Comando.ExecuteNonQuery());
            //  if (cuantos > 0)
            //{
            //MessageBox.Show("Cambio de clave exitoso", "Info");
            //this.DialogResult = DialogResult.Retry;
            //}
            //else
            //{
            //MessageBox.Show("Error en cambio de clave", "Error");
            //this.DialogResult = DialogResult.Retry;
            //}


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}