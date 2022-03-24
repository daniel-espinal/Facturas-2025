using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class CargosVarios
    {
        decimal hc_clave_abonado;
        decimal hc_cod_trans;
        string cod_designacion;
        decimal hc_valor;

        public decimal Hc_clave_abonado { get => hc_clave_abonado; set => hc_clave_abonado = value; }
        public decimal Hc_cod_trans { get => hc_cod_trans; set => hc_cod_trans = value; }
        public string Cod_designacion { get => cod_designacion; set => cod_designacion = value; }
        public decimal Hc_valor { get => hc_valor; set => hc_valor = value; }

        public List<CargosVarios> ObtenerCargosVarios(string mesFacturacion)
        {
            List<CargosVarios> lstCargosVarios = new List<CargosVarios>();
            Conexion conexion = new Conexion();

            try
            {
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(Util.Util.SP_CARGOS_VARIOS, conexion.conexion);
                if (conexion.ConectarServer())
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.Parameters.Add("@ULTIMO_MES_FACTURA", SqlDbType.VarChar).Value = mesFacturacion;
                    dataAdapter.Fill(dataTable);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        CargosVarios cargosVarios = new CargosVarios();

                        cargosVarios.Hc_clave_abonado = (decimal)dataTable.Rows[i]["hc_clave_abonado"];
                        cargosVarios.Hc_cod_trans = (decimal)dataTable.Rows[i]["hc_cod_trans"];
                        cargosVarios.Cod_designacion = dataTable.Rows[i]["cod_designacion"].ToString();
                        cargosVarios.Hc_valor = (decimal)dataTable.Rows[i]["hc_valor"];

                        lstCargosVarios.Add(cargosVarios);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return lstCargosVarios;

        }
    }
}
