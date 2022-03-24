using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class Reactiva
    {
        decimal hc_clave_abonado;
        decimal reactiva;

        public decimal Hc_clave_abonado { get => hc_clave_abonado; set => hc_clave_abonado = value; }
        public decimal Reactiva1 { get => reactiva; set => reactiva = value; }

         public List<Reactiva> ObtenerReactiva(string mesFacturacion)
        {
            List<Reactiva> lstReactiva = new List<Reactiva>();

            Conexion conexion = new Conexion();

            try
            {
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(Util.Util.SP_REACTIVA, conexion.conexion);
                if (conexion.ConectarServer())
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.Parameters.Add("@ULTIMO_MES_FACTURA", SqlDbType.VarChar).Value = mesFacturacion;
                    dataAdapter.Fill(dataTable);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Reactiva reactiva = new Reactiva();
                            reactiva.Hc_clave_abonado = (decimal)dataTable.Rows[i]["hc_clave_abonado"];
                            reactiva.Reactiva1 = (decimal)dataTable.Rows[i]["reactiva"];
                        lstReactiva.Add(reactiva);

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
           


            return lstReactiva;
        }


    }
}
