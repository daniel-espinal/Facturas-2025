using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class HistoricoConsumo
    {
        decimal m_clave_primaria;
        int MES;
        decimal m_consumo;

        public decimal Clave_primaria { get => m_clave_primaria; set => m_clave_primaria = value; }
        public int MES1 { get => MES; set => MES = value; }
        public decimal Consumo { get => m_consumo; set => m_consumo = value; }

        public List<HistoricoConsumo> obtenerHistoricoConsumo()
        {
            List<HistoricoConsumo> listaHistorica = new List<HistoricoConsumo>();

            Conexion conexion = new Conexion();

            try
            {
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(Util.Util.SP_HISTORICO_CONSUMO, conexion.conexion);
                if (conexion.ConectarServer())
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.Fill(dataTable);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        HistoricoConsumo hc = new HistoricoConsumo();

                        hc.Clave_primaria = (decimal)dataTable.Rows[i]["m_clave_primaria"];
                        hc.MES1 = (int)dataTable.Rows[i]["MES"];
                        hc.Consumo = (decimal)dataTable.Rows[i]["m_consumos1"];

                        listaHistorica.Add(hc);
                    }
                }
            }
            catch(Exception ex)
            {
                //PARAMETRIZAR EX
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return listaHistorica;
        }
    }
}
