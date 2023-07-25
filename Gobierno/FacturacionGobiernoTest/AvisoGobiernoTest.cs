using FacturacionGobierno.Model;
using FacturacionGobierno.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobiernoTest
{
    [TestClass]     
   public class AvisoGobiernoTest
    {
        [TestMethod]       
        public void ObtenerAvisoGobierno()
        {
            Conexion conexion = new Conexion();


            String vTexto = "6".PadLeft(4, '0');
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conexion.conexion;
              //  command.CommandText = Util.AVISOS_GOBIERNO;

                if (conexion.ConectarServer())
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            decimal clave_abonado = (decimal)dataReader["SUFIJO"];
                            Console.WriteLine("{0}", clave_abonado);
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
