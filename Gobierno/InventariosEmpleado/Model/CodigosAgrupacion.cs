using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class CodigosAgrupacion
    {
        double codigo;
        string descripcion;

        public double Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public List<CodigosAgrupacion> ObtenerFacturaAgrupacion()
        {
            List<CodigosAgrupacion> listCodigosAgrupacion = new List<CodigosAgrupacion>();
            DataTable dataTable = new DataTable();
            Conexion conexion = new Conexion();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conexion.conexion;
                command.CommandText = Util.Util.CODIGO_AGRUPACION_GOB_CAB_PADRE;
                if (conexion.ConectarServer())
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CodigosAgrupacion codigosAgrupacion = new CodigosAgrupacion();
                            codigosAgrupacion.Codigo = Convert.ToDouble(reader["gc_cod_agrup_eeh"]);
                            codigosAgrupacion.Descripcion = reader["gc_nombre_inst"].ToString();
                            listCodigosAgrupacion.Add(codigosAgrupacion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return listCodigosAgrupacion;

        }

        public List<CodigosAgrupacion> ObtenerCodigosAgrupacion()
        {
            List<CodigosAgrupacion> listCodigosAgrupacion = new List<CodigosAgrupacion>();
            
            DataTable dataTable = new DataTable();
            Conexion conexion = new Conexion();
            try
            {
               
                SqlDataAdapter dataAdapter = new SqlDataAdapter(Util.Util.SP_CODIGOS_AGRUPACION, conexion.conexion);
                if (conexion.ConectarServer())
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.Fill(dataTable);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        CodigosAgrupacion codigosAgrupacion = new CodigosAgrupacion();
                        codigosAgrupacion.Codigo = (double) dataTable.Rows[i]["a_codigo"];
                        codigosAgrupacion.Descripcion = dataTable.Rows[i]["a_descripcion"].ToString();
                        listCodigosAgrupacion.Add(codigosAgrupacion);
                    }
                }
            } catch(Exception ex){
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listCodigosAgrupacion;
        }
    }
}
