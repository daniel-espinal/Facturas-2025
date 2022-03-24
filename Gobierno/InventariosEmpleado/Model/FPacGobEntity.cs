using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class FPacGobEntity
    {

        decimal hc_ciclo;
        decimal hc_clave_abonado;
        decimal hc_cod_trans;

        public decimal Hc_ciclo { get => hc_ciclo; set => hc_ciclo = value; }
        public decimal Hc_clave_abonado { get => hc_clave_abonado; set => hc_clave_abonado = value; }
        public decimal Hc_cod_trans { get => hc_cod_trans; set => hc_cod_trans = value; }


        public List<FPacGobEntity> ObtenerlistFPacGob()
        {
            List<FPacGobEntity> listFPacGob = new List<FPacGobEntity>();

            Conexion conexion = new Conexion();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conexion.conexion;
                command.CommandText = Util.Util.FACTURACION_DBO_FPACGOB;
                command.Parameters.Add("@Clave", SqlDbType.Decimal);
                command.Parameters["@Clave"].Value = 1424802;
                command.Parameters.Add("@Trans", SqlDbType.Decimal);
                command.Parameters["@Trans"].Value = 224;
                
                if (conexion.ConectarServer())
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FPacGobEntity entity = new FPacGobEntity();
                            entity.Hc_ciclo = (decimal)reader["hc_ciclo"];
                            entity.Hc_clave_abonado = (decimal)reader["hc_clave_abonado"];
                            entity.Hc_cod_trans = (decimal)reader["hc_cod_trans"];
                            listFPacGob.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Lanzar una EX Controlada
            }
            finally
            {
                conexion.Close();
            }

            return listFPacGob;
        }

    }
}
